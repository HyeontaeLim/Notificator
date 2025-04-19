using Microsoft.AspNetCore.Mvc;
using static Notificator.Models.ChzzkModel;

namespace Notificator.ViewComponents;

public class LiveListViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(LiveListRs liveList)
    {
        ViewData["NextPage"] = liveList.content.page.next;
        return View(liveList);
    }
}