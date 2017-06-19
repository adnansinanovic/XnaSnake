using System.Collections.Generic;
using Common.KeyboardManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    public class Snake : GameComponent
    {
        private Game _game;
        private List<SnakeTailItem> _tail;
        private decimal _speed;
        private Sprite _sprTail;
        private SpriteBatch _spriteBatch;
        private Rectangle _playground;        

        private GameTime _currentGameTime;
        private long _lastMovementTime;
        private Direction _direction;
        private bool _run;        

        public Snake(Game game, Sprite sprSnakeTail, SpriteBatch spriteBatch, Rectangle playground) : base(game)
        {
            _game = game;            

            _tail = new List<SnakeTailItem>();
            _playground = playground;                                    
            
            _sprTail = sprSnakeTail;
            _spriteBatch = spriteBatch;
            Reset();            
        }    

        public List<SnakeTailItem> Tail
        {
            get { return _tail; }
        }

        public decimal Speed
        {
            get { return decimal.Round((1/_speed)*1000, 1); }            
        }

        public int PartWidth
        {
            get { return _sprTail.Width; }            
        }

        public int PartHeight
        {
            get { return _sprTail.Height; }
        }

        public int SnakeLength
        {
            get { return _tail.Count; }            
        }

        public override void Update(GameTime gameTime)
        {                                                 
            _currentGameTime = gameTime;

            if (_run)
            {
                if (_currentGameTime.TotalGameTime.TotalMilliseconds - _lastMovementTime >= (double) _speed)
                {
                    _lastMovementTime = (long)_currentGameTime.TotalGameTime.TotalMilliseconds;
                    MoveSnake();
                } 
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                ChangeDirection(Direction.Up);
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                ChangeDirection(Direction.Down);
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                ChangeDirection(Direction.Left);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                ChangeDirection(Direction.Right);
            else if (Keyboard.GetState().IsKeyDown(Keys.Add))
            {
                if (_speed - 10 > 0)
                    _speed-=10;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Subtract))                            
                    _speed+=10;                            

            base.Update(gameTime);
        }

        public void RemoveTail()
        {
            RemoveTail(1);
        }

        public void RemoveTail(int count)
        {
            if (count > 0)
            {
                if (_tail.Count - count > 2)
                    _tail.RemoveRange(_tail.Count - 1 - count, count);
                else if (_tail.Count > 2)
                    _tail.RemoveRange(2, _tail.Count - 2);
            }
        }

        private void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    if (_direction != Direction.Up)
                        _direction = Direction.Down;                    
                    break;    
                case Direction.Up:
                    if (_direction != Direction.Down)
                        _direction = Direction.Up;
                    break;    
                case Direction.Left:
                    if (_direction != Direction.Right)
                        _direction = Direction.Left;
                    break;
                case Direction.Right:
                    if (_direction != Direction.Left)
                        _direction = Direction.Right;
                    break;
            }
        }

        private void MoveSnake()
        {         
            if (_tail.Count > 1)
            {
                for (int i = _tail.Count - 1; i > 0; i--)
                        _tail[i].SetPosition(_tail[i-1].Position);                                         
            }
         
            int x = (int)_tail[0].X;
            int y = (int)_tail[0].Y;

            MovePosition(ref x, ref y);

            AdjustPosition(ref x, ref y);

            _tail[0].SetPosition(x,y);            
        }

        private void Debug(string value)
        {
            System.Diagnostics.Trace.WriteLine(value + ". -----------------------");
            foreach (SnakeTailItem item in _tail)
            {
                System.Diagnostics.Trace.WriteLine(item.X + " " + item.Y);
            }
        }

        private void MovePosition(ref int x, ref int y)
        {            
            switch (_direction)
            {
                case Direction.Left:
                    x = (int) (_tail[0].X - _sprTail.Width);
                    break;
                case Direction.Right:
                    x = (int) (_tail[0].X + _sprTail.Width);
                    break;
                case Direction.Down:
                    y = (int) (_tail[0].Y + _sprTail.Height);
                    break;
                case Direction.Up:
                    y = (int) (_tail[0].Y - _sprTail.Height);
                    break;
            }         
        }

        private void AdjustPosition(ref int x, ref int y)
        {
            if (x > _playground.Right)
                x = _playground.Left;
            else if (x < _playground.Left)
                x = _playground.Right;

            if (y > _playground.Bottom)
                y = _playground.Top;
            else if (y < _playground.Top)
                y = _playground.Bottom;            
        }

        public void Draw()
        {        
            foreach (SnakeTailItem tail in _tail)            
                _sprTail.Draw(tail.Position);                            
        }

        public void AddTail()
        {
            int x = (int) _tail[0].X;
            int y = (int) _tail[0].Y;
            
            MovePosition(ref x,ref y);
            AdjustPosition(ref x, ref y);
            
            _tail.Add(new SnakeTailItem(_game,_spriteBatch,new Vector2(x,y),_sprTail));                
        }

        public bool CheckCollisionWithItSelf()
        {                               
            for (int i=3; i<_tail.Count; i++)
                if (_tail[0].Bounds.Intersects(_tail[i].Bounds))
                return true;

            return false;
        }       

        public bool CheckColision(Rectangle rectangle)
       {            
            bool result = _tail[0].Bounds.Intersects(rectangle);

            return result;

        }

        public void Run()
        {
            _run = true;
        }

        public void Stop()
        {
            _run = false;
        }


        public void Reverse()
        {
            _tail.Reverse();
            ReverseDirection();
        }

        private void ReverseDirection()
        {
            Rectangle up = _tail[0].Bounds;
            Rectangle down = _tail[0].Bounds;
            Rectangle left = _tail[0].Bounds;
            Rectangle right = _tail[0].Bounds;

            up.Y -= _tail[0].Height;
            down.Y += _tail[0].Height;
            left.X -= _tail[0].Width;
            right.X += _tail[0].Width;

            if (_tail[1].Bounds.Intersects(up))
                _direction = Direction.Down;
            else if (_tail[1].Bounds.Intersects(down))
                _direction = Direction.Up;
            else if (_tail[1].Bounds.Intersects(left))
                _direction = Direction.Right;
            else if (_tail[1].Bounds.Intersects(right))
                _direction = Direction.Left;            
        }


        public void IncreaseSpeed(int speedIncrease)
        {
            decimal currentSpeed = this.Speed;
            decimal targetSpeed = currentSpeed + speedIncrease;
           
            if (targetSpeed > 0)
            {
                decimal newSpeed = (1 / targetSpeed) * 1000;
                _speed = newSpeed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">(miliseconds)</param>
        public void SetSpeed(int value)
        {
            _speed = value;
        }

        public void Reset()
        {
            _speed = 100;
            _tail.Clear();
            
            _tail.Add(new SnakeTailItem(_game, _spriteBatch, new Vector2(50, 210), _sprTail));          
            AddTail();

            _speed = 100;

            _lastMovementTime = 0;
            _direction = Direction.Right;
            _run = false;
        }
    }
}

