/*
(c) Copyright Ascensio System SIA 2010-2014

This program is a free software product.
You can redistribute it and/or modify it under the terms 
of the GNU Affero General Public License (AGPL) version 3 as published by the Free Software
Foundation. In accordance with Section 7(a) of the GNU AGPL its Section 15 shall be amended
to the effect that Ascensio System SIA expressly excludes the warranty of non-infringement of 
any third-party rights.

This program is distributed WITHOUT ANY WARRANTY; without even the implied warranty 
of MERCHANTABILITY or FITNESS FOR A PARTICULAR  PURPOSE. For details, see 
the GNU AGPL at: http://www.gnu.org/licenses/agpl-3.0.html

You can contact Ascensio System SIA at Lubanas st. 125a-25, Riga, Latvia, EU, LV-1021.

The  interactive user interfaces in modified source and object code versions of the Program must 
display Appropriate Legal Notices, as required under Section 5 of the GNU AGPL version 3.
 
Pursuant to Section 7(b) of the License you must retain the original Product logo when 
distributing the program. Pursuant to Section 7(e) we decline to grant you any rights under 
trademark law for use of our trademarks.
 
All the Product's GUI elements, including illustrations and icon sets, as well as technical writing
content are licensed under the terms of the Creative Commons Attribution-ShareAlike 4.0
International. See the License terms at http://creativecommons.org/licenses/by-sa/4.0/legalcode
*/

// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.38967
//    <NameSpace>ASC.Mail.DomainParser</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>True</IncludeSerializeMethod><UseBaseClass>True</UseBaseClass><GenBaseClass>True</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net35</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>True</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>True</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>True</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace ASC.Mail.Aggregator
{
    using System.IO;
    using System.Collections.Generic;


    #region Base entity class
    public partial class EntityBase<T>
    {

        private static System.Xml.Serialization.XmlSerializer serializer;

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                }
                return serializer;
            }
        }

        #region Serialize/Deserialize
        /// <summary>
        /// Serializes current EntityBase object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize()
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes workflow markup into an EntityBase object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out T obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static T Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((T)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current EntityBase object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an EntityBase object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output EntityBase object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, out T obj, out System.Exception exception)
        {
            exception = null;
            obj = default(T);
            try
            {
                obj = LoadFromFile(fileName);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out T obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static T LoadFromFile(string fileName)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion
    }
    #endregion

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class clientConfig : EntityBase<clientConfig>
    {
        private clientConfigEmailProvider emailProviderField;

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool versionSpecified { get; set; }

        /// <summary>
        /// clientConfig class constructor
        /// </summary>
        public clientConfig()
        {
            this.emailProviderField = new clientConfigEmailProvider();
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public clientConfigEmailProvider emailProvider
        {
            get
            {
                return this.emailProviderField;
            }
            set
            {
                this.emailProviderField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clientConfigEmailProvider : EntityBase<clientConfigEmailProvider>
    {
        private List<string> domainField;

        private List<clientConfigEmailProviderIncomingServer> incomingServerField;

        private List<clientConfigEmailProviderOutgoingServer> outgoingServerField;

        private clientConfigEmailProviderDocumentation documentationField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string displayName { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string displayShortName { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id { get; set; }


        /// <summary>
        /// clientConfigEmailProvider class constructor
        /// </summary>
        public clientConfigEmailProvider()
        {
            this.documentationField = new clientConfigEmailProviderDocumentation();
            this.outgoingServerField = new List<clientConfigEmailProviderOutgoingServer>();
            this.incomingServerField = new List<clientConfigEmailProviderIncomingServer>();
            this.domainField = new List<string>();
        }

        [System.Xml.Serialization.XmlElementAttribute("domain", Order = 0)]
        public List<string> domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute("incomingServer", Order = 3)]
        public List<clientConfigEmailProviderIncomingServer> incomingServer
        {
            get
            {
                return this.incomingServerField;
            }
            set
            {
                this.incomingServerField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public List<clientConfigEmailProviderOutgoingServer> outgoingServer
        {
            get
            {
                return this.outgoingServerField;
            }
            set
            {
                this.outgoingServerField = value;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public clientConfigEmailProviderDocumentation documentation
        {
            get
            {
                return this.documentationField;
            }
            set
            {
                this.documentationField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clientConfigEmailProviderIncomingServer : EntityBase<clientConfigEmailProviderIncomingServer>
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string hostname { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int port { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string socketType { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string authentication { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string username { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clientConfigEmailProviderOutgoingServer : EntityBase<clientConfigEmailProviderOutgoingServer>
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string hostname { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int port { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string socketType { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string authentication { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string username { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class clientConfigEmailProviderDocumentation : EntityBase<clientConfigEmailProviderDocumentation>
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url { get; set; }
    }
}
