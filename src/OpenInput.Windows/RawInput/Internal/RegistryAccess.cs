﻿namespace OpenInput.RawInput
{
    using Microsoft.Win32;

    static class RegistryAccess
    {
        public static RegistryKey GetDeviceKey(string device)
        {
            var split = device.Substring(4).Split('#');

            var classCode = split[0];       // ACPI (Class code)
            var subClassCode = split[1];    // PNP0303 (SubClass code)
            var protocolCode = split[2];    // 3&13c0b0c5&0 (Protocol code)

            return Registry.LocalMachine.OpenSubKey(string.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", classCode, subClassCode, protocolCode));
        }

        public static string GetClassType(string classGuid)
        {
            var classGuidKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\" + classGuid);
            return classGuidKey != null ? (string)classGuidKey.GetValue("Class") : string.Empty;
        }
    }
}
