using System.ComponentModel.DataAnnotations;

namespace SpotPicker.Model
{
    public class ExampleTable
    {
        public ExampleTable()
        {
        }

        public ExampleTable( string? exampleName, string? exampleValue )
        {
            ExampleID = 0;
            ExampleName = exampleName;
            ExampleValue = exampleValue;
        }
        [Key]
        public int ExampleID { get; set; }
        public string? ExampleName { get; set; }
        public string? ExampleValue { get; set; } 
    }
}
