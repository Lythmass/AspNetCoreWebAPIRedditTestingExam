namespace Reddit.Dtos
{
    public class CreateCommunityDto
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
