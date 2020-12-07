namespace TasksApi.Pagination
{
    public class TaskParameters
    {
        public int Page { get; set; } = 1;

        const int maxPageSize = 50;
        private int size = 10;

        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = (value >= maxPageSize) ? maxPageSize : value;
            }
        }
    }
}