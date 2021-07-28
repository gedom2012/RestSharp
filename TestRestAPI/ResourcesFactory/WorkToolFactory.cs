using System;
using System.Collections.Generic;
using System.Text;

namespace TestRestAPI.ResourcesFactory
{
    class WorkToolFactory: TestSamplesBase
    {
        public WorkTool WorkTool()
        {
            var workTool = new WorkTool
            {
                Name = "Ferramenta de teste 03",
                SerialNumber = "9876541111",
                IsActive = true,
                ExternalPartner = new ExternalPartner { Id = 10, SiteId = 2 },
                WorkToolType = new WorkToolType { Id = 1, SiteId = 2 }

            };
            return workTool;
        }
    }
}
