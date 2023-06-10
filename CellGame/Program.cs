

using System.Diagnostics;
using System.IO;

var fileList = new List<string>()
{
    "bin/Sango.CellGame.Console.deps.json",
    "bin/Sango.CellGame.Console.dll",
    "bin/Sango.CellGame.Console.exe",
    "bin/Sango.CellGame.Console.pdb",
    "bin/Sango.CellGame.Console.runtimeconfig.json",
    "bin/Sango.CellGame.Core.dll",
    "bin/Sango.CellGame.Core.pdb"
};

var success = true;

foreach (var file in fileList.Where(file => !File.Exists(Path.Combine(file))))
{
    Console.WriteLine($"ERROR: 目标文件({file})不存在！");
    success = false;
}

if (!success)
{
    Console.WriteLine("未能成功找到所有文件，按任意键退出\n");
    Console.ReadKey();
    return -1;
}

var p = Process.Start("bin/Sango.CellGame.Console.exe");
p.WaitForExit();

return 0;
