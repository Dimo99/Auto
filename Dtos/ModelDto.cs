namespace Dtos
{
    public class ModelDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }

        public override bool Equals(object obj)
        {
            ModelDto other = (ModelDto)obj;

            if (other == null)
            {
                return false;
            }

            if (this.Name.ToLower() == other.Name.ToLower() && this.BrandId == other.BrandId)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 * this.Name.ToLower().GetHashCode();
            hash = hash * 23 * this.BrandId.GetHashCode();
            return hash;
        }
    }
}
