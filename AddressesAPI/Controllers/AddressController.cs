using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AddressesAPI.Models;

namespace AddressesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private Address address = new Address();
        private readonly ILogger<AddressController> _logger;
        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{searchString}")]
        public List<string> SearchAddresses(string searchString)
        {
            List<string> addresses = new List<string>();
            addresses = address.Search(searchString);
            LogSearch(addresses.Count());
            return addresses;
        }

        [HttpPost("{newAddress}")]
        public void CreateNewAddress(string newAddress)
        {
            
            if (address.VerificationNewAddress(newAddress) == true)
            {
                address.AddAddress(newAddress);
            }
            LogAddAddresses(address.VerificationNewAddress(newAddress), newAddress);
        }

        private void LogAddAddresses(bool verificationAddress, string newAddress)
        {
            if (verificationAddress)
            {
                _logger.LogInformation("Add address" + newAddress);
            }
            else
            {
                _logger.LogError("The string does not match the criterion");
            }
        }

        private void LogSearch(int quantityAddresses)
        {
            if (quantityAddresses != 0)
            {
                _logger.LogInformation("Found " + quantityAddresses + "addresses");
            }
            else
            {
                _logger.LogInformation("Not found");
            }
        }
    }
}
