namespace MasterChef.Service
{
    using MasterChef.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;

    [DataContract]
    public class RecipeInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ICollection<PreparationStep> PreparationSteps { get; set; }
    }
}