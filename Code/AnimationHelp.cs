namespace PeanutToTheDungeon
{
    public static class AnimationHelp
    {
        public static int[] FindCloserPoint(Point startPoint, Point endPoint)
        {
            //If the testPoint needs to change only along the Y axis
            if(endPoint.X == startPoint.X)
            {
                //If the endPoint's Y is higher than startPoint, return a higher point
                if(endPoint.Y < startPoint.Y)
                {
                    return new int[] { startPoint.X, startPoint.Y + 1, 0, 1 };
                }
                //Otherwise return a lower point
                else
                {
                    return new int[] { startPoint.X, startPoint.Y - 1, 0, -1 };
                }
            }
            //If the testPoint needs to change only along the X axis
            else if(endPoint.Y == startPoint.Y)
            {
                //If endPoint is to the right of startPoint, return a point towards the right
                if(endPoint.X > startPoint.X)
                {
                    return new int[] { startPoint.X - 1, startPoint.Y, -1, 0 };
                }
                //Otherwise return a point to the left
                else
                {
                    return new int[] { startPoint.X + 1, startPoint.Y, 1, 0 };
                }
            }
            //If the point is to the top right, return a point to the top right
            if(IsTopRight(startPoint, endPoint))
            {
                return new int[] { startPoint.X - 1, startPoint.Y - 1, -1, -1 };
            }
            //If the point is to the top left, return a point to the top left
            else if(IsTopLeft(startPoint, endPoint))
            {
                return new int[] { startPoint.X + 1, startPoint.Y - 1, 1, -1 };
            }
            //If the point is to the bottom right, return a point to the bottom right
            else if(IsBottomRight(startPoint, endPoint))
            {
                return new int[] { startPoint.X - 1, startPoint.Y + 1, -1, 1 };
            }
            //If the point is to the bottom left, return a point to the bottom left
            else if(IsBottomLeft(startPoint, endPoint))
            {
                return new int[] { startPoint.X + 1, startPoint.Y + 1, 1, 1 };
            }
            return new int[] { endPoint.X, endPoint.Y, 0, 0 };
        }

        //Checks if a point is close enough to another point, used for peanut to stop jittering around trying to get to the correct position when sleeping
        public static bool IsClose(Point testPoint, Point endPoint)
        {
            if(Math.Abs(endPoint.X) - Math.Abs(testPoint.X) <= 2 && Math.Abs(endPoint.X) - Math.Abs(testPoint.X) >= -2)
            {
                if(Math.Abs(endPoint.Y) - Math.Abs(testPoint.Y) <= 2 && Math.Abs(endPoint.Y) - Math.Abs(testPoint.Y) >= -2)
                {
                    return true;
                }
            }
            return false;
        }


        //Checks if the testPoint is to the left of the startPoint
        public static bool IsLeft(Point startPoint, Point testPoint)
        {
            if(startPoint.X > testPoint.X)
            {
                return true;
            }
            return false;
        }

        //Checks if the testPoint is to the top right of startPoint
        private static bool IsTopRight(Point startPoint, Point testPoint)
        {
            if(startPoint.X < testPoint.X && startPoint.Y < testPoint.Y)
            {
                return true;
            }
            return false;
        }

        //Checks if the testPoint is to the top left of startPoint
        private static bool IsTopLeft(Point startPoint, Point testPoint)
        {
            if(startPoint.X > testPoint.X && startPoint.Y < testPoint.Y)
            {
                return true;
            }
            return false;
        }

        //Checks if the testPoint is to the bottom right of startPoint
        private static bool IsBottomRight(Point startPoint, Point testPoint)
        {
            if(startPoint.X < testPoint.X && startPoint.Y > testPoint.Y)
            {
                return true;
            }
            return false;
        }

        //Checks if the testPoint is to the bottom left of startPoint
        private static bool IsBottomLeft(Point startPoint, Point testPoint)
        {
            if(startPoint.X > testPoint.X && startPoint.Y > testPoint.Y)
            {
                return true;
            }
            return false;
        }
    }
}