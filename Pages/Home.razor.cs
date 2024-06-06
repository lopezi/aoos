using ArweaveBlazor;

namespace FlowbiteBlazorWasmStarter.Pages
{
    public partial class Home
    {
        int _step = 0;
        string? _jwk = null;
        string? _address = null;
        string? _resultId = null;
        string? _resultMsg = null;
        string? _tokenResult = null;

        string morpheus = "-xmOkXs3b6vMnQmdMw4tasHVZwG3_d7kEiUJa9lp8_w";
        private string inputName { get; set; } = string.Empty;
        public void Step(int step)
        {
            _step = step;
        }

        public async Task GenerateWallet()
        {
            _jwk = await ArweaveService.GenerateWallet();
            _address = await ArweaveService.GetAddress(_jwk);
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

            //TODO: Send to process
            _tokenResult = "tokenId";

        }

        public async Task CheckBalance()
        {
            _step = 4;

        }

        public async Task GoToOS()
        {
            NavigationManager.NavigateTo("/os");

        }
    }
}
