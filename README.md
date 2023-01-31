# TexMachineSimulation.HTK
This project is an evolution of the [**MachineSimulation.NET**](https://github.com/federicocoppa75/MachineSimulation.NET) where the link are moved by the value of the variables of cnc [**Tex**](https://www.texautomation.it/): the target is simulare the machine for debug and develop the plc.

## CncViewer
Application for view the machine with relative tooling.

## CncViewer.ConfigEditor
Application for edit the association bethween machine model links and plc variables.

## CncViewer.ConfigEditor.DataSource.File.Xml
Class library used for the serializzation by [CncViewer.ConfigEditor](#CncViewer.ConfigEditor), used to read and write link-variable association.

## CncViewer.ConfigEditor.ViewModels
Class library of the ViewModels of [CncViewer.ConfigEditor](#CncViewer.ConfigEditor).

## CncViewer.Connection
Class library of the ViewModels of [CncViewer](#CncViewer).

## CncViewer.Connection.Bridge
Class library for specialization of the ViewModels of [CncViewer.Connection](#CncViewer.Connection) for Tex cnc comunication.

## CncViewer.Connection.DataSource.File.Xml
Class library used for the serializzation by [CncViewer](#CncViewer) used to read link-variable association.

## CncViewer.Connection.Interfaces
Class library used to define the interfaces of the ViewModels of [CncViewer.Connection](#CncViewer.Connection).

## CncViewer.Connection.Views
Class library used to define the view of the variable list read fron the cnc in [CncViewer](#CncViewer).

## CncViewer.Models
Class library used to define the data model used to describe the association bethween links and variables.
