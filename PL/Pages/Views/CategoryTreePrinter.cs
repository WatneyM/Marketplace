using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Text;
using DSL.Adapters.Category;

namespace Application.Pages.Views
{
    public class CategoryTreePrinter : ViewComponent
    {
        private StringBuilder result = new StringBuilder();

        private string a = $"<div class=\"hierarchy-col\">";
        private string b = $"</div>";

        private string c = """
            <div class="row my-1 ai-baseline">
            <div class="col normalize-hierarchy-col" style="--i: {0}rem;">
            <p>{1}</p>
            </div>
            <div class="col">
            <p>{2}</p>
            </div>
            <div class="col">
            <p>{3}</p>
            </div>
            <div class="col">
            <a class="link" href="/maintenance/category?category={4}">Edit</a>
            </div>
            </div>
            """;
        private string c1 = """
            <div class="row my-1 ai-baseline">
            <div class="col normalize-hierarchy-col" style="--i: {0}rem;">
            <p>{1}</p>
            </div>
            <div class="col">
            <p>{2}</p>
            </div>
            <div class="col">
            <p>{3}</p>
            </div>
            <div class="col">
            <a class="link" href="/maintenance/category?category={4}">Edit</a>
            <a class="link mx-2"
            href="/maintenance/category?category={4}&handler=Drop">Remove</a>
            </div>
            </div>
            """;

        public IViewComponentResult Invoke(List<CategoryRAdapter> categories)
        {
            Prepare(categories);

            return new HtmlContentViewComponentResult(
                new HtmlString(result.ToString()));
        }

        private void Prepare(IEnumerable<CategoryRAdapter> origin,
            string key = null!, int layer = 0)
        {
            bool isStraight;

            if (key is null)
            {
                foreach (var item in origin.Where(filter
                => filter.AttachedToCategory == null))
                {
                    if (!item.Attachable)
                    {
                        result.Append(string.Format(c, 0, item.Category, item.CreatedAt, item.ModifiedAt, item.Key));
                        result.Append(a);
                        Prepare(origin, item.Key!, layer + 1);
                        result.Append(b);
                    }
                    if (item.AttachedToCategory == null && item.Attachable)
                        result.Append(string.Format(c1, 0, item.Category, item.CreatedAt, item.ModifiedAt, item.Key));
                }
            }
            else
            {
                foreach (var item in origin.Where(filter
                => filter.AttachedToCategory == key))
                {
                    isStraight = true;
                    if (!item.Attachable)
                    {
                        result.Append(string.Format(c, 1.114 * layer, item.Category, item.CreatedAt, item.ModifiedAt, item.Key));
                        result.Append(a);
                        Prepare(origin, item.Key!, layer + 1);
                        result.Append(b);
                        isStraight = false;
                    }
                    if (isStraight)
                        result.Append(string.Format(c1, 1.105 * layer, item.Category, item.CreatedAt, item.ModifiedAt, item.Key));
                }
            }
        }
    }
}