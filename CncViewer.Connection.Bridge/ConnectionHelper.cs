using GZSoft.Tex.Controller.Communication;
using GZSoft.Tex.Controller.Compiler;
using GZSoft.Tex.Controller.Interface;
using GZSoft.Tex.Controller.PowerFamily.Ftp;
using GZSoft.Tex.Controller.Variable;
using GZSoft.Tex.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MVMIoc = Machine.ViewModels.Ioc;
using MVMUI = Machine.ViewModels.UI;

namespace CncViewer.Connection.Bridge
{
    internal static class ConnectionHelper
    {
        public static byte[] SendFrame(IController controller, byte[] frame, int timeout)
        {
            ICommunicationSocket socket;

            socket = (ICommunicationSocket)controller.Socket;
            return socket.SendFrame(frame, timeout);
        }

        public static string SendFrame(IController controller, string strFrame, Encoding enc, int timeout)
        {
            byte[] frame;

            frame = enc.GetBytes(strFrame);
            frame = SendFrame(controller, frame, timeout);

            return enc.GetString(frame);
        }

        public static string SendFrame(IController controller, string strFrame, Encoding enc)
        {
            return SendFrame(controller, strFrame, enc, 1000);
        }

        public static string SendFrame(IController controller, string strFrame, int timeout)
        {
            return SendFrame(controller, strFrame, Encoding.GetEncoding("iso-8859-1"), timeout);
        }

        public static string SendFrame(IController controller, string strFrame)
        {
            return SendFrame(controller, strFrame, Encoding.GetEncoding("iso-8859-1"), 1000);
        }

        public static string Compile(IController controller, Type type)
        {
            IFile file = (IFile)controller.GetControllerService(type);
            string strMessage = string.Empty;

            if (file is ICompilable)
            {
                CompileErrorCollection errors = (file as ICompilable).Compile();

                if (errors != null && errors.Count > 0)
                {
                    foreach (CompileError err in errors)
                        strMessage += err.Text + "\n";
                }
            }

            return strMessage;
        }

        public static void ReceiveFile(IController controller, Type type, ref Stream stream)
        {
            IFile file = (IFile)controller.GetControllerService(type);

            if (file != null && file.CanDownload)
                stream = file.RawDownload();
        }

        public static void TransmitFile(IController controller, Type type, Stream stream)
        {
            IFile file = (IFile)controller.GetControllerService(type);

            if (file != null && file.CanUpload)
                file.RawUpload(stream);
        }

        public static void UploadFirmware(IController controller, string path)
        {
            IFirmwareFile file = (IFirmwareFile)controller.GetControllerService(typeof(IFirmwareFile));

            if (file != null && file.CanUpload)
                file.RawUpload(path);
        }

        public static int GetVariableW(IController controller, SubSystem env, int index)
        {
            IIntegerVariableService var = (IIntegerVariableService)controller.GetControllerService(typeof(IIntegerVariableService));

            return var.Read(env, index);
        }

        public static void SetVariableW(IController controller, SubSystem env, int index, int value)
        {
            IIntegerVariableService var = (IIntegerVariableService)controller.GetControllerService(typeof(IIntegerVariableService));

            var.Write(env, index, value);
        }

        public static FixedPoint GetVariableV(IController controller, SubSystem env, int index)
        {
            IFixedPointVariableService var = (IFixedPointVariableService)controller.GetControllerService(typeof(IFixedPointVariableService));

            return var.Read(env, index);
        }

        public static void SetVariableV(IController controller, SubSystem env, int index, FixedPoint value)
        {
            IFixedPointVariableService var = (IFixedPointVariableService)controller.GetControllerService(typeof(IFixedPointVariableService));

            var.Write(env, index, value);
        }

        public static double GetVariableD(IController controller, SubSystem env, int index)
        {
            IFloatingPointVariableService var = (IFloatingPointVariableService)controller.GetControllerService(typeof(IFloatingPointVariableService));

            return var.Read(env, index);
        }

        public static void SetVariableD(IController controller, SubSystem env, int index, double value)
        {
            IFloatingPointVariableService var = (IFloatingPointVariableService)controller.GetControllerService(typeof(IFloatingPointVariableService));

            var.Write(env, index, value);
        }

        public static bool GetBitS(IController controller, SubSystem env, int index)
        {
            ISystemBitVariableService var = (ISystemBitVariableService)controller.GetControllerService(typeof(ISystemBitVariableService));

            return var.Read(env, index);
        }

        public static void SetBitS(IController controller, SubSystem env, int index, bool value)
        {
            ISystemBitVariableService var = (ISystemBitVariableService)controller.GetControllerService(typeof(ISystemBitVariableService));

            var.Write(env, index, value);
        }

        public static bool GetBitR(IController controller, SubSystem env, int index)
        {
            IPersistentBitVariableService var = (IPersistentBitVariableService)controller.GetControllerService(typeof(IPersistentBitVariableService));

            return var.Read(env, index);
        }

        public static void SetBitR(IController controller, SubSystem env, int index, bool value)
        {
            IPersistentBitVariableService var = (IPersistentBitVariableService)controller.GetControllerService(typeof(IPersistentBitVariableService));

            var.Write(env, index, value);
        }

        public static bool GetBitF(IController controller, SubSystem env, int index)
        {
            IVolatileBitVariableService var = (IVolatileBitVariableService)controller.GetControllerService(typeof(IVolatileBitVariableService));

            return var.Read(env, index);
        }

        public static void SetBitF(IController controller, SubSystem env, int index, bool value)
        {
            IVolatileBitVariableService var = (IVolatileBitVariableService)controller.GetControllerService(typeof(IVolatileBitVariableService));

            var.Write(env, index, value);
        }

        public static bool GetBitO(IController controller, SubSystem env, int index)
        {
            IDigitalOutputService var = (IDigitalOutputService)controller.GetControllerService(typeof(IDigitalOutputService));

            return var.Read(env, index);
        }

        public static void SetBitO(IController controller, SubSystem env, int index, bool value)
        {
            IDigitalOutputService var = (IDigitalOutputService)controller.GetControllerService(typeof(IDigitalOutputService));

            var.Write(env, index, value);
        }

        public static bool GetBitI(IController controller, SubSystem env, int index)
        {
            IDigitalInputService var = (IDigitalInputService)controller.GetControllerService(typeof(IDigitalInputService));

            return var.Read(env, index);
        }

        //public static void SetBitI(IController controller, SubSystem env, int index, bool value)
        //{
        //    IDigitalInputService var = (IDigitalInputService)controller.GetControllerService(typeof(IDigitalInputService));

        //    var.Write(env, index, value);
        //}

        public static int GetInfo(IController controller, char axis, int index)
        {
            IIntegerInfoService var = (IIntegerInfoService)controller.GetControllerService(typeof(IIntegerInfoService));

            if (char.IsLetter(axis))
                return var.Read(axis, index);

            return var.Read(index);
        }

        public static FixedPoint GetFinfo(IController controller, char axis, int index)
        {
            IFixedPointInfoService var = (IFixedPointInfoService)controller.GetControllerService(typeof(IFixedPointInfoService));

            if (char.IsLetter(axis))
                return var.Read(axis, index);

            return var.Read(index);
        }

        public static double GetDinfo(IController controller, char axis, int index)
        {
            IFloatingPointInfoService var = (IFloatingPointInfoService)controller.GetControllerService(typeof(IFloatingPointInfoService));

            if (char.IsLetter(axis))
                return var.Read(axis, index);

            return var.Read(index);
        }

        public static string GetSinfo(IController controller, int index)
        {
            IStringInfoService var = (IStringInfoService)controller.GetControllerService(typeof(IStringInfoService));

            return var.Read(index);
        }

        public static void SetSinfo(IController controller, SubSystem subSystem, char axis, int index, string value)
        {
            IStringInfoService var = (IStringInfoService)controller.GetControllerService(typeof(IStringInfoService));

            //var.Write(index, value);
            var.Write(subSystem, index, value);
        }

        public static string GetParametroMacchina(IController controller, int index)
        {
            IParameterService par = (IParameterService)controller.GetControllerService(typeof(IParameterService));

            return par.Read(index);
        }

        public static void SetParametroMacchina(IController controller, int index, string value)
        {
            IParameterService par = (IParameterService)controller.GetControllerService(typeof(IParameterService));

            par.Write(index, value);
        }

        public static string GetParametroMacchinaAsse(IController controller, char axis, int index)
        {
            IParameterService par = (IParameterService)controller.GetControllerService(typeof(IParameterService));

            return par.Read(axis, index);
        }

        public static void SetParametroMacchinaAsse(IController controller, char axis, int index, string value)
        {
            IParameterService par = (IParameterService)controller.GetControllerService(typeof(IParameterService));

            par.Write(axis, index, value);
        }

        public static void ReceiveFtpFile(IController controller, string source, string dest)
        {
            IFtpService ftp = (IFtpService)controller.GetControllerService(typeof(IFtpService));

            string unit = source.Substring(0, 2);
            FtpDirectoryInfo dirInfo = ftp.GetDirectoryInfo(unit);

            if (dirInfo.Exists == false)
                throw (new Exception("L'unità \"" + unit + "\" non è al momento disponibile"));

            FtpFileInfo fileInfo = ftp.GetFileInfo(source);

            if (!fileInfo.Exists)
                throw (new Exception("Il file \"" + source + "\" non esiste"));

            FtpFileStream ftpFs = fileInfo.OpenRead();
            FileStream fs = new FileStream(dest, FileMode.Create);

            ftpFs.CopyTo(fs);
            fs.Close();
            ftpFs.Close();
        }

        public static void TransmitFtpFile(IController controller, string source, string dest)
        {
            IFtpService ftp = (IFtpService)controller.GetControllerService(typeof(IFtpService));

            string unit = dest.Substring(0, 2);
            FtpDirectoryInfo dirInfo = ftp.GetDirectoryInfo(unit);

            if (dirInfo.Exists == false)
                throw (new Exception("L'unità \"" + unit + "\" non è al momento disponibile"));

            dirInfo = ftp.GetDirectoryInfo(Path.GetDirectoryName(dest));
            dirInfo.Create();

            FtpFileInfo fileInfo = ftp.GetFileInfo(dest);
            FtpFileStream ftpFs = fileInfo.OpenWrite();
            FileStream fs = new FileStream(source, FileMode.Open);

            fs.CopyTo(ftpFs);
            fs.Close();
            ftpFs.Close();
        }

        public static void TransmitVrmFile(IController _controller, int nProg, string fileName)
        {
            try
            {
                int n, nBytes, nFrame;
                byte[] fileBytes;
                byte[] frameBytes;

                fileBytes = File.ReadAllBytes(fileName);
                nBytes = 0;
                nFrame = 0;

                frameBytes = Encoding.Default.GetBytes(String.Format("Tr-{0:D}*{1:D}", nProg, fileBytes.Length));
                frameBytes = ConnectionHelper.SendFrame(_controller, frameBytes, 5000);

                if (frameBytes[0] != 6)
                    throw new Exception("Command error");

                while (nBytes < fileBytes.Length)
                {
                    if (fileBytes.Length - nBytes > 1024)
                        n = 1024;
                    else
                        n = fileBytes.Length - nBytes;

                    frameBytes = Encoding.Default.GetBytes(String.Format("Er-{0:D4}", nFrame));
                    Array.Resize(ref frameBytes, frameBytes.Length + n);
                    Array.Copy(fileBytes, nBytes, frameBytes, 7, n);

                    frameBytes = ConnectionHelper.SendFrame(_controller, frameBytes, 5000);

                    if (frameBytes[0] != 6)
                        throw new Exception("Transmit error");

                    nBytes += n;
                    nFrame++;
                }

                frameBytes = Encoding.Default.GetBytes("F--");
                frameBytes = ConnectionHelper.SendFrame(_controller, frameBytes, 5000);

                if (frameBytes[0] != 6)
                    throw new Exception("Transmit error");
            }
            catch (Exception ex)
            {
                MVMIoc.SimpleIoc<MVMUI.IExceptionObserver>.GetInstance().NotifyException(ex);
            }
        }

        public static void ReceiveVrmFile(IController _controller, int nProg, string fileName)
        {
            try
            {
                int nBytes, nFrame;
                byte[] fileBytes;
                byte[] frameBytes;

                nFrame = 0;
                nBytes = 0;
                fileBytes = new byte[0];

                while (true)
                {
                    if (nFrame == 0)
                        frameBytes = Encoding.Default.GetBytes(String.Format("Rr-{0:D}*{1:D}", nProg, nFrame));
                    else
                        frameBytes = Encoding.Default.GetBytes(String.Format("Rr-{0:D}", nFrame));

                    frameBytes = ConnectionHelper.SendFrame(_controller, frameBytes, 5000);

                    if (frameBytes.Length > 3 && frameBytes[0] == 'E' && frameBytes[1] == '-' && frameBytes[2] == '-')
                    {
                        Array.Resize(ref fileBytes, nBytes + frameBytes.Length - 7);
                        Array.Copy(frameBytes, 7, fileBytes, nBytes, frameBytes.Length - 7);
                        nBytes += frameBytes.Length - 7;
                        nFrame++;
                    }
                    else if (frameBytes.Length == 3 && frameBytes[0] == 'F' && frameBytes[1] == '-' && frameBytes[2] == '-')
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception("Receive error");
                    }
                }

                File.WriteAllBytes(fileName, fileBytes);
            }
            catch (Exception ex)
            {
                MVMIoc.SimpleIoc<MVMUI.IExceptionObserver>.GetInstance().NotifyException(ex);
            }
        }
    }

}
