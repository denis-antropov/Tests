namespace PairOfNumbers
{
    using System;

    /// <summary>
    /// Defines a pair
    /// </summary>
    public struct ValuesPair : IEquatable<ValuesPair>
    {
        /// <summary>
        /// Gets or sets first value of pair
        /// </summary>
        public int Value1 { get; set; }

        /// <summary>
        /// Gets or sets second value of pair
        /// </summary>
        public int Value2 { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(ValuesPair other)
        {
            return Value1 == other.Value1 && Value2 == other.Value2 ||
                Value1 == other.Value2 && Value2 == other.Value1;
        }
    }
}
