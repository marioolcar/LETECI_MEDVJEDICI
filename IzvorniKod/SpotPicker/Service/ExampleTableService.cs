using SpotPicker.Model;
using SpotPicker.Service.Interface;

namespace SpotPicker.Service
{
    public class ExampleTableService : IExampleTableService
    {
        private readonly SpotPickerContext _context;

        public ExampleTableService(SpotPickerContext context)
        {
            _context = context;
        }

        public async Task<ExampleTable> CreateExampleTable(ExampleTable exampleTable)
        {
            await _context.AddAsync(exampleTable);
            await _context.SaveChangesAsync();
            return exampleTable;
        }
    }
}
