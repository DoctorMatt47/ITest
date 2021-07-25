using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ITest.Attributes
{
    public class CollectionCountAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;
        
        public CollectionCountAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }
        
        public override bool IsValid(object? value) => value switch
        {
            null => true,
            ICollection collection => _min < collection.Count && collection.Count < _max,
            _ => false
        };
    }
}