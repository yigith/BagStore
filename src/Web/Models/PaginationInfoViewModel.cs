namespace Web.Models
{
    public class PaginationInfoViewModel
    {
        public PaginationInfoViewModel(int page, int itemsOnPage, int totalItems)
        {
            Page = page;
            ItemsOnPage = itemsOnPage;
            TotalItems = totalItems;
        }

        public int Page { get; set; }

        public int ItemsOnPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / Constants.ITEMS_PER_PAGE);

        public bool HasPrevious => Page > 1;

        public bool HasNext => Page < TotalPages;

        public int RangeStart => (Page - 1) * Constants.ITEMS_PER_PAGE + 1;

        public int RangeEnd => RangeStart + ItemsOnPage - 1;

        public int[] PageNumbers => Pagination(Page, TotalPages);

        // https://gist.github.com/kottenator/9d936eb3e4e3c3e02598
        private int[] Pagination(int current, int last)
        {
            int delta = 1;
            int left = current - delta;
            int right = current + delta + 1;
            var range = new List<int>();
            var rangeWithDots = new List<int>();
            int? l = null;

            for (var i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var i in range)
            {
                if (l != null)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots.Add(l.Value + 1);
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots.Add(-1);
                    }
                }
                rangeWithDots.Add(i);
                l = i;
            }

            return rangeWithDots.ToArray();
        }
    }
}
