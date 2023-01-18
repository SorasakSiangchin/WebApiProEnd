namespace WebApiProjectEnd.Installers
{
    public static class InstallerExtensions
    {
        //สร้างส่วนขยาย Service (C# extension)
        public static void MyInstallerExtensions(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //ทำการแสกนหาตัวที่สืบทอดมาจาก interface
            var installers = typeof(Program).Assembly.ExportedTypes.Where(x =>
            // มีตัวไหนบ้างที่สืบมาจาก IInstallers *****
                typeof(IInstallers).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                //*****
                .Select(Activator.CreateInstance).Cast<IInstallers>().ToList();

            //ทำการวนรูบและการลงทะเบียน 
            //คือ DI
            installers.ForEach(installer => installer.InstallServices(builder));
        }

    }
}
