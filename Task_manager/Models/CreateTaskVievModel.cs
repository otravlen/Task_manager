using Task_manager.Enum;




namespace Task_manager.Models
{
    public class CreateTaskVievModel
    {
        public long Id { get; set; }    
        public string Name { get; set; }

        public bool IsDone { get; set; }

        public Priority Priority { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укажите название задачи!");
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentNullException(Description, "Укажите описание задачи!");
            }
        }
        
        

    }
}
