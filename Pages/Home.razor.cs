using ArweaveBlazor;
using ArweaveBlazor.Models;
using System.Net;

namespace FlowbiteBlazorWasmStarter.Pages
{
    public partial class Home
    {
        int _step = 0;
        string? _jwk = null;
        string? _address = null;
        string? _resultId = null;
        string? _resultMsg = null;
        string? _newTokenId = null;
        string? _tokenResult = null;

        string morpheus = "-xmOkXs3b6vMnQmdMw4tasHVZwG3_d7kEiUJa9lp8_w";
        private string inputName { get; set; } = string.Empty;
        private string tokenName { get; set; } = string.Empty;
        private string tokenTicker { get; set; } = string.Empty;

        private MemValues MemValues { get; set; } = new MemValues();
        public void Step(int step)
        {
            _step = step;
        }

        public async Task GenerateWallet()
        {
            _jwk = await ArweaveService.GenerateWallet();
            _address = await ArweaveService.GetAddress(_jwk);


            MemValues.Address = _address;
            await StorageService.SaveMemValues(MemValues);
        }

        public async Task DownloadWallet()
        {
            if (_jwk == null)
            {
                return;
            }

            var result = await ArweaveService.SaveFile($"{_address}.json", _jwk);

            _step = 2;
        }

        public async Task TalkToMorpheus()
        {
            if (_jwk == null)
            {
                return;
            }

            _resultId = await ArweaveService.SendAsync(_jwk, morpheus, null, null, new List<ArweaveBlazor.Models.Tag>
            {
                new ArweaveBlazor.Models.Tag { Name = "Action", Value = "talk"},
                new ArweaveBlazor.Models.Tag { Name = "Name", Value = inputName},
            });

        }

        public async Task CheckResult()
        {
            if (_resultId == null)
            {
                return;
            }

            //TODO: Send to process
            _step = 3;

            _resultMsg = await ArweaveService.GetResultAsync<string>(morpheus, _resultId);

        }

        public async Task CreateToken()
        {
            if (_resultId == null)
            {
                return;
            }

            //string data = "local bint = require('.bint')(256)\r\nlocal ao = require('ao')\r\nlocal json = require('json')\r\n\r\nHandlers.add('talk', Handlers.utils.hasMatchingTag('Action', 'talk'),\r\n  function(msg) \r\n    \r\n    ao.send({ Target = msg.From, Data = \"Hi \" .. msg.Tags.Name .. \", are you The One? Can you create a token for me?\"}) \r\nend)";
            string data = EmbeddedResourceReader.ReadResource("FlowbiteBlazorWasmStarter.token.txt");
            data = data.Replace("ao.id", $"\"{_address}\"");
            data = data.Replace("_NAME_", tokenName);
            data = data.Replace("_TICKER_", tokenTicker);

            Console.WriteLine(tokenName);
            Console.WriteLine(tokenTicker);
            Console.WriteLine(data);

            _newTokenId = await ArweaveService.CreateProcess(_jwk, "nI_jcZgPd0rcsnjaHtaaJPpMCW847ou-3RGA5_W3aZg");
            Console.WriteLine("processId: " + _newTokenId);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var dataId = await ArweaveService.SendAsync(_jwk, _newTokenId, _address, data, new List<Tag>
            {
                new Tag() { Name = "Action", Value = "Eval"}
            });
            Console.WriteLine("DataId: " + dataId);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var testResult = await ArweaveService.SendAsync(_jwk, _newTokenId, null, null, new List<ArweaveBlazor.Models.Tag>
            {
                new ArweaveBlazor.Models.Tag { Name = "Target", Value = _newTokenId},
                new ArweaveBlazor.Models.Tag { Name = "Action", Value = "Transfer"},
                new ArweaveBlazor.Models.Tag { Name = "Quantity", Value = "10000"},
                new ArweaveBlazor.Models.Tag { Name = "Recipient", Value = _address},
            });
            Console.WriteLine("testResult: " + testResult);

            _tokenResult = _newTokenId;

            MemValues.Token = _newTokenId;
            await StorageService.SaveMemValues(MemValues);

            _step = 4;

        }

        public async Task GoToOS()
        {
            NavigationManager.NavigateTo("/os");

        }
    }
}
