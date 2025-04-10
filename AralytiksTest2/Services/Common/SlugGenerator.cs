namespace AralytiksTest2.Services.Common
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;

            string slug = title.ToLowerInvariant()
                .Replace(" ", "-")
                .Replace("[^a-z0-9-]", "")
                .Trim('-');

            return string.IsNullOrEmpty(slug) ? $"post-{Guid.NewGuid()}" : slug.Length > 100 ? $"{slug.Substring(0, 100)}-{Guid.NewGuid()}" : $"{slug}-{Guid.NewGuid()}";
        }
    }
}
