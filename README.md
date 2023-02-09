# TexMachineSimulation.HTK
This project is an evolution of the [**MachineSimulation.NET**](https://github.com/federicocoppa75/MachineSimulation.NET) where the link are moved by the value of the variables of cnc [**Tex automation**](https://www.texautomation.it/): the target is simulare the machine for debug and develop the plc.

## CncViewer
Application for view the machine with relative tooling.
To view the simulation it is necessary load the machine (file *.json) and relative tooling (file *.jTooling) or environment (file *.jEnv), then it is necessary to load the variable-link file (file *.xCncLink): now could be established the connectione with the cnc to view the machine movements. 

## CncViewer.ConfigEditor
Application for edit the association bethween machine model links and plc variables (file *.xCncLink). To create a file you could start from loading a machine file (file *.json): this is for import the list of the links of the machine; ad this point it is possible to link each link with relative cnc variable.

## CncViewer.ConfigEditor.DataSource.File.Xml
Class library used for the serializzation by [CncViewer.ConfigEditor](#CncViewer.ConfigEditor), used to read and write link-variable association.

## CncViewer.ConfigEditor.ViewModels
Class library of the ViewModels of [CncViewer.ConfigEditor](#CncViewer.ConfigEditor).

## CncViewer.Connection
Class library of the ViewModels of [CncViewer](#CncViewer). The main class is *ConnectionViewModel*: it is the container of the *VariableViewModel* instances these contain the associations bethween links and cnc variables.

## CncViewer.Connection.Bridge
Class library for specialization of the ViewModels of [CncViewer.Connection](#CncViewer.Connection) for Tex cnc comunication: in this class library is loadded the comunication library of [**Tex automation**](https://www.texautomation.it/) (TexControllers.dll); this has to be placed in the project subfolder *.\Comunication library*.

## CncViewer.Connection.DataSource.File.Xml
Class library used for the serializzation by [CncViewer](#CncViewer) used to read link-variable association.

## CncViewer.Connection.Interfaces
Class library used to define the interfaces of the ViewModels of [CncViewer.Connection](#CncViewer.Connection).

## CncViewer.Connection.Views
Class library used to define the view of the variable list read fron the cnc in [CncViewer](#CncViewer).

## CncViewer.Models
Class library used to define the data model used to describe the association bethween links and variables.
