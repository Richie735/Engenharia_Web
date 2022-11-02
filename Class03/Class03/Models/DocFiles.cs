namespace Class03.Models
{
    public class DocFiles
    {
        //this method gets all filenames from folder instead of
        //getting them from a database or other storage kind
        public List<FileViewModel> GetFiles(IHostEnvironment e)
        {
            List<FileViewModel> list = new List<FileViewModel>();

            //get all information from "Documents" folder
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(e.ContentRootPath, "wwwroot/Documents"));

            //use the information from folder to get the filenames
            foreach(var item in dirInfo.GetFiles()){
                list.Add(new FileViewModel
                {
                    Name = item.Name,
                    Size = item.Length
                });
            }
            return list;
        }
    }
}
