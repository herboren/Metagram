# Metagram
Extracts a general subset of metadata from RAW only files, and generates an RGB histogram per image. Useful for Astrophotography beginners understanding the best level of exposure to use in their projects. Metadata extraction based on https://github.com/drewnoakes/metadata-extractor Only key property tags were taken that appeared relevant to the job and help others to identify pertinent information to shooting appropriate frames by identifying the metadata within the files. This would allow the photographer to adjust their camera settings according to the best quality images within this tool on their next project.

A series of frames should be shot from chosen focal lengths, at certain ISO's, at certain F/Stops, and exposure times to identify the best quality image for the job to be stacked later. Below are just some examples to follow, you may use your own settings. 

- **ISO 800**
  - FStop: 4.0 
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 5.0 
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 5.6
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 6.3
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 7.1
    - x1 of each Exposures: 10, 15, 20, 25
  - ...

- **ISO 1600**
  - FStop: 4.0 
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 5.0 
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 5.6
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 6.3
    - x1 of each Exposures: 10, 15, 20, 25
  - FStop: 7.1
    - x1 of each Exposures: 10, 15, 20, 25
  - ...

Upon selecting a file in the list, the histogram will be generated accordingly
![image](https://user-images.githubusercontent.com/987794/235722305-8d5751dc-1c4d-40f7-9f0a-d2de79791c0f.png)

#Marking best photo with color!

The user can mark at most 3 photos with Good, Better, Best quality when working with larger quantity of files as the naming convention of files generated by the camera can be quite confusing. MetaData for each file is stored within the textbox for easier identification of each photos metadata.

Use a context menu to mark the appropriate files, delete, open file location, or even copy metadata to a formatted string for use in forum threads or personal use. This has only been tested on Canon CR2 formats and few others, however, unsupproted formats are beyond my control and should be addressed with https://github.com/drewnoakes/metadata-extractor

![image](https://user-images.githubusercontent.com/987794/235715436-444926a6-257b-4c78-8fe4-093163b1a2ed.png)

Only supported files will have their meta data extracted. Unsupported files will result in an error. Common RAW files have been added however, the final result may actually be different, some formats may not be supported by the API:
- **.cr2**:
	*Canon Raw 2 Image*
- **.rw2**:
	*Panasonic RAW Image*
- **.raf**:
	*Fujifilm RAW Image*
- **.erf**:
	*Epson RAW File*
- **.nrw**:
	*Nikon Raw Image*
- **.nef**:
	*Nikon Electronic Format RAW Image*
- **.arw**:
	*Sony Alpha Raw Digital Camera Image*
- **.rwz**:
	*Rawzor Compressed Image*
- **.dng**:
	*Digital Negative Image*
- **.eip**:
	*Enhanced Image Package File*
- **.bay**:
	*Casio RAW Image*
- **.dcr**:
	*Kodak Digital Camera RAW Image*
- **.gpr**:
	*GoPro RAW Image*
- **.raw**:
	*Raw Image Data*
- **.crw**:
	*Canon Raw CIFF Image File*
- **.3fr**:
	*Hasselblad 3F RAW Image*
- **.sr2**:
	*Sony RAW Image*
- **.k25**:
	*Kodak K25 Image*
- **.kc2**:
	*Kodak DCS200 Camera Raw Image*
- **.mef**:
	*Mamiya RAW Image*
- **.dng**:
	*Apple ProRAW Image*
- **.cs1**:
	*CaptureShop 1-shot Raw Image*
- **.orf**:
	*Olympus RAW File*
- **.mos**:
	*Leaf Camera RAW Image*
- **.ari**:
	*ARRIRAW Image*
- **.cr3**:
	*Canon Raw 3 Image File*
- **.kdc**:
	*Kodak Photo-Enhancer File*
- **.fff**:
	*Hasselblad RAW Image*
- **.srf**:
	*Sony RAW Image*
- **.srw**:
	*Samsung RAW Image*
- **.j6i**:
	*Ricoh Camera Image File*
- **.mfw**:
	*Mamiya Camera Raw File*
- **.x3f**:
	*SIGMA X3F Camera RAW File*
- **.rwl**:
	*Leica RAW Image*
- **.pef**:
	*Pentax Electronic File*
- **.iiq**:
	*Phase One RAW Image*
- **.cxi**:
	*FMAT RAW Image*
- **.nks**:
	*Nikon Capture NX-D Sidecar File*
- **.mdc**:
	*Minolta Camera Raw Image*
