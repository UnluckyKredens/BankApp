using AuthInfrastructure.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthInfrastructure.DTOS
{
    public class AddAttributeDTOS
    {
        public UserAttributeReadModel[] Attributes { get; set; }
    }
}
