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

            //TODO: Send to process
            _resultId = "TODO";

        }

        public async Task CheckResult()
        {
            if (_resultId == null)
            {
                return;
            }

            //TODO: Send to process
            _step = 3;

            _resultMsg = "TODO";

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
