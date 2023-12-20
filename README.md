# IOLink.NET

**DISCLAIMER: IOLink.NET is currently under development and not yet feature complete. As soon as end-to-end functionality can be provide we will publish nuget packages and detailed code samples**

IOLink.NET aims to be a comprehensive yet lean library for interacting with [IO-Link](https://io-link.com/en/Technology/what_is_IO-Link.php?thisID=76) devices for the .NET ecosystem.

IOLink.NET comes with everything needed to convert IO-Link data into human-readable data and vice versa. The following IO-Link data will be supported:

|              | Read | Write |
| ------------ | :--: | :---: |
| Process Data |  ✅  |  🕒   |
| Parameter    |  ✅  |  🕒   |
| Events       |  🕒  |  ❌   |

IOLink.NET also provides a parser for the IO Device Description or short [IODD](https://io-link.com/share/Downloads/Spec-IODD/IO-Device-Desc-Spec_10012_V113_Mar22.zip) format that allows you to automatically extract all the data types a given IO-Link device uses.

## Supporters

As every project that requires hardware components to be integrated we need devices to test our vendor specific implemen very much for supporting our community project.

| Name                | Support                                                                 |
| ------------------- | ----------------------------------------------------------------------- |
| ifm electronic gmbh | Kindly provided us with a starter set of their IoT IOLink Master AL1350 |
