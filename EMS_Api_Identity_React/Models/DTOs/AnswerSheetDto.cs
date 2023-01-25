namespace EMS_Api_Identity_React.Models.DTOs
{
    public class AnswerSheetDto
    {
        public int AnswerSheetId { get; set; }
        public bool IsChecked { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
