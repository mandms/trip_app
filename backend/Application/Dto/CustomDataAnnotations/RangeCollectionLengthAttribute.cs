using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.CustomDataAnnotations
{
    public class RangeCollectionLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public RangeCollectionLengthAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            if (value is IEnumerable collection)
            {
                int count = 0;

                foreach (var item in collection)
                {
                    count++;
                }

                return count >= _minLength && count <= _maxLength;
            }

            return true;
        }
    }

}
