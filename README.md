# IOLink.NET

**DISCLAIMER: IOLink.NET is currently under development and not yet feature complete. As soon as end-to-end functionality can be provide we will publish nuget packages and detailed code samples**

![CI Workflow](https://github.com/domdeger/IOLink.NET/actions/workflows/ci.yml/badge.svg)

IOLink.NET aims to be a comprehensive yet lean library for interacting with [IO-Link](https://io-link.com/en/Technology/what_is_IO-Link.php?thisID=76) devices for the .NET ecosystem.

IOLink.NET comes with everything needed to convert IO-Link data into human-readable data and vice versa. The following IO-Link data will be supported:

|              | Read | Write |
| ------------ | :--: | :---: |
| Process Data |  ✅  |  🕒   |
| Parameter    |  ✅  |  🕒   |
| Events       |  🕒  |  ❌   |

IOLink.NET also provides a parser for the IO Device Description or short [IODD](https://io-link.com/share/Downloads/Spec-IODD/IO-Device-Desc-Spec_10012_V113_Mar22.zip) format that allows you to automatically extract all the data types a given IO-Link device uses.

## Getting started

IOLinkNET offers a modular toolset to work with IOLink data and device descriptions. Since we do not know the requirements specific to your project we have provide you with different components that you are free to orchestrate in order to achieve your goals. Otherwise we also maintain a default implementation.

Different Usage samples are provided in the samples/ folder. It is still work in progress but growing steadily.

## Overview

Decoding IO-Link data requires us to complete different workloads before hands. First of all we need to source the IODD package for the device we are working with. Then the IODD package has to be searched for the correct description file which in turn has be to parsed. The raw IODD format has some shortcomings when it comes to automatic processing so it needs to be preprocessed.

Based on the preprocessed IODD structure we are able to select the correct data types and decode/encode the given payloads. As you notice there is plenty of work to do for IOLinkNET. In this section we describe the functionality of the different projects.

| Package                           | Purpose                                                                                                                                    |
| --------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| IOLinkNET.IODD.Structure          | Contains model to represent the XML structure of an IODD.                                                                                  |
| IOLinkNET.IODD.Provider           | Functionality to retrieve IODD packages from e.g. the IODDFinderAPI                                                                        |
| IOLinkNET.IODD.Parser             | Parser components to transform XML IODD to IOLinkNET.IODD.Structure format                                                                 |
| IOLinkNET.IODD.Resolution         | Defines favorable and self-contained format for data type representation and utility to create those from IOLinkNET.IODD.Structure format. |
| IOLinkNET.IODD.Standard           | Contains IODD Standard components to support IO-Link Standards                                                                             |
| IOLinkNET.Conversion              | Defines functionality to convert from and to iolink data.                                                                                  |
| IOLinkNET.Device                  | Defines contracts how communication with io link masters.                                                                                  |
| IOLinkNET.Vendors.\*              | Provides vendor specific implementation of the Device interfaces.                                                                          |
| IOLinkNET.Integration             | Orchestrates the modules to functionality that can read process data or parameter data from a device implementation.                       |
| IOLinkNET.Visualization           | Base implementation to provide easy to use components which are useful for data visualization e.g. device readable menu structure          |
| IOLinkNET.Visualization.Structure | Contains visualization models which are more usable than using IODD structures with references                                             |

## Supporters

As every project that requires hardware components to be integrated we need devices to test our vendor specific implementation against real devices. We highly appreciate every support from the community to source those devices and broaden the hardware support of IOLink.NET. This section is dedicated to thank the supports of the project!

| Name                | Support                                                                 |
| ------------------- | ----------------------------------------------------------------------- |
| ifm electronic gmbh | Kindly provided us with a starter set of their IoT IOLink Master AL1350 |
