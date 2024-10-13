namespace CommonTools
{
    public class TimeDivider(int divideCount)
    {
        private int _tickCounter = 0;

        public bool TickProcess()
        {
            if (++_tickCounter % divideCount != 0)
            {
                return false;
            }

            _tickCounter = 0;
            return true;
        }
    }
}
