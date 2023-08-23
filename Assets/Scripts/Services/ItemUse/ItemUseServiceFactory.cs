namespace Services.ItemUse
{
	public class ItemUseServiceFactory 
	{

		public static IItemUseService Create()
		{
			var itemUseService = new ItemUseService();
			itemUseService.AddRequirement((user, item) => item.IsReady());
			itemUseService.AddEffect((user, item) => item.Use(user));
			return itemUseService;
		}
	}
}