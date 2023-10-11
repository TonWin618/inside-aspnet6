namespace App
{
    public interface IFileSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="print">int为层级，string为路径</param>
        void ShowStructure(Action<int, string> print);
    }
}
