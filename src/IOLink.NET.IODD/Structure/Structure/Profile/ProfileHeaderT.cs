namespace IOLink.NET.IODD.Structure.Profile;

public record ProfileHeaderT(string ProfileIdentification = "IO Device Profile", string ProfileRevision = "1.1",
                             string ProfileName = "Device Profile for IO Devices", string ProfileSource = "IO-Link Consortium", string ProfileClassID = "Device");
