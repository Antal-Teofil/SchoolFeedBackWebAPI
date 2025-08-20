using FeedBackApp.Core.QuestionnaireUtils.ReviewUtils.ReviewContracts;

namespace FeedBackApp.Core.QuestionnaireUtils.ReviewUtils
{
    public sealed class RecipientContainer<TRecipient> : IReadOnlyList<TRecipient> where TRecipient : IRecipientType
    {
        private readonly List<TRecipient> _items;
        public RecipientContainer(IEnumerable<TRecipient> items) => _items = items.ToList();

        public int Count => _items.Count;
        public TRecipient this[int index] => _items[index];
        public IEnumerator<TRecipient> GetEnumerator() => _items.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
