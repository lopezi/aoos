using Blazored.LocalStorage;

namespace FlowbiteBlazorWasmStarter
{
    public class StorageService
    {
        private readonly ILocalStorageService localStorage;

        private const string MemValuesKey = "MemValues";
        public StorageService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async ValueTask<MemValues> GetMemValues()
        {
            var result = await localStorage.GetItemAsync<MemValues>(MemValuesKey);
            return result ?? new();
        }


        public ValueTask SaveMemValues(MemValues list)
        {
            return localStorage.SetItemAsync(MemValuesKey, list);
        }

    }
}
