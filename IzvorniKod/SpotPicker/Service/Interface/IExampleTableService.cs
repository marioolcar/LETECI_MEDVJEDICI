using SpotPicker.Model;

namespace SpotPicker.Service.Interface
{
    public interface IExampleTableService
    {
        public Task<ExampleTable> CreateExampleTable(ExampleTable exampleTable);
    }
}
