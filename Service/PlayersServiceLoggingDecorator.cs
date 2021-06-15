using decorator_design_pattern.Contract;
using decorator_design_pattern.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace decorator_pattern.Service
{
    public class PlayersServiceLoggingDecorator : IPlayersService
    {
        private readonly IPlayersService _playersService;
        private readonly ILogger<PlayersServiceLoggingDecorator> _logger;

        public PlayersServiceLoggingDecorator(IPlayersService playersService,
            ILogger<PlayersServiceLoggingDecorator> logger)
        {
            _playersService = playersService;
            _logger = logger;
        }

        public IEnumerable<Player> GetPlayersList()
        {
            _logger.LogInformation("Starting to fetch data");

            var stopwatch = Stopwatch.StartNew();

            IEnumerable<Player> players = _playersService.GetPlayersList();

            foreach (var player in players)
            {
                _logger.LogInformation("Player: " + player.Id + ", Name: " + player.Name);
            }

            stopwatch.Stop();

            var elapsedTime = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation($"Finished fetching data in {elapsedTime} milliseconds");

            return players;
        }
    }
}
