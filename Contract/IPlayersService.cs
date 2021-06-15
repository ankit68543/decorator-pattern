using decorator_design_pattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace decorator_design_pattern.Contract
{
    public interface IPlayersService
    {   
        IEnumerable<Player> GetPlayersList();
    }
}
