import JsonFile from '../../appsettings.json';

export  class  Configuration 
{
    static getConfiguration(ConfigurationName: string): object | undefined 
    {
        if(JsonFile.hasOwnProperty(ConfigurationName))
        {
            return Object.getOwnPropertyDescriptor(JsonFile, ConfigurationName)?.value;           
        }

        return undefined;
    }
}