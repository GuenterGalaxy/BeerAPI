namespace Domain.Entities;
public record Beer(int Id, string BrandName, string Name, List<Article> Articles, string DescriptionText);