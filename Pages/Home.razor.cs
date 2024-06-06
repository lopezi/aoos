using ArweaveBlazor;

namespace aoos.Pages
{
    public partial class Home
    {
        int _step = 0;
        string? _jwk = null;
        string? _address = null;

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
        }
    }
}
