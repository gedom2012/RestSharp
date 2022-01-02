namespace TestRestAPI.ResourcesFactory
{
    class WorkToolFactory : TestSamplesBase
    {
        public static WorkTool CreateWorkTool()
        {
            var obj = new WorkTool
            {
                Name = "Work Tool test 01",
                SerialNumber = "8888888888",
                IsActive = true,
                ExternalPartner = new ExternalPartner { Id = 10, SiteId = 2 },
                WorkToolType = new WorkToolType { Id = 1, SiteId = 2 }

            };
            return obj;
        }

        public static WorkTool EditWorkTool()
        {
            var obj = new WorkTool
            {
                Name = "Ferramenta de teste 02",
                SerialNumber = "99999999",
                IsActive = false,
                ExternalPartner = new ExternalPartner { Id = 10, SiteId = 2 },
                WorkToolType = new WorkToolType { Id = 1, SiteId = 2 }

            };
            return obj;
        }
    }
}
