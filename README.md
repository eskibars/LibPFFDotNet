## LibPFFDotNet
A .NET library to use the [libpff](https://github.com/libyal/libpff) project along with a sample application to preview the e-mails and export a PFF file (OST, PST) to .eml files on disk.

## Importing and Compilation
1. Download [libpff](https://github.com/libyal/libpff) and compile.  Alternatively, I have uploaded versions which I have compiled for Windows here: [64-bit](https://drive.google.com/file/d/0B0B_HvrE-vjRZzY2SjRNUWJCTGs/view?usp=sharing) or [32-bit](https://drive.google.com/file/d/0B0B_HvrE-vjRQ0E4M1pOc21uMGs/view?usp=sharing)
2. Create your project or open the PFF-converter project, depending on what you want to do
3. Drag and drop the compiled .dlls (libpff.dll and zlib.dll if you built in Visual Studio) to copy them into the project
4. In Visual Studio, set the "Copy to Output Directory" property for each of the .dlls in the project to "Copy always"

If you're simply compiling the existing project, you are now done and can compile at this stage.

If you're adding LibPFFDotNet to your own project, do the above steps and then:

1. Add the "LibPFFDotNet" project into your solution by going to File->Add->Existing Project
2. Add a reference to LibPFFDotNet to your own project by going to Project->Add Reference and then selecting "LibPFFDoNet" from the Solution->Projects tab
3. In any lass that you want to use LibPFFDotNet, it's unsurprisingly held in the namespace "LibPFFDotNet", which you'll need to either imporing using "using LibPFFDotNet" or making specific references to the sub-classes

You should now be able to compile your project

## Sample
The general process to open and use a PFF (.ost or .pst) file is as follows:

    PFF pFile = new PFF();
    pFile.Open(@"C:\myfile.ost");
    PFFFolder pFolder = pFile.GetRootFolder();
    List<PFFFolder> pSubfolders = pFolder.GetSubfolders();
    for (int i = 0; i < pSubfolders.Count; i++)
    {
        string folderName = string name = pSubfolders[i].GetName();
        // do something intelligent here...  for example, you can recurse by accessing pSubfolders[i].GetSubfolders()
    }
    pFolder.Export(@"C:\export"); // export all folders and messages recursively
    
    List<PFFMessage> pMessages = pFolder.GetSubmessages(); // in reality, this particular folder probably won't contain anything, simply because the root folder is unlikely to house data... but let's suppose it does...
    PFFMessage pFirstMessage = pMessages[0];
    
    string htmlMessage = pFirstMessage.GetBody(PFFMessage.BodyType.HTML);
    /*
     * Two notes: 
     * 1) There are a few types to choose from.. text, HTML, and RTF.  Not every message will have all 3.  Right now, nothing intelligent is done to return a non-preferred type
     * 2) The HTML body will attempt to render images in the message by converting them to base64 objects and replacing the image content ID source (<img src="cid:..." ...>) with a data source (<img src="data:image/..." ...>)
    */
    List<PFFAttachment> pAttachments = pFirstMessage.GetAttachments();
    PFFRecipients pRecipients = pFirstMessage.GetRecipients();

## Limitations
Lots of limitations right now...

1. Only exports mail messages, attachments, and folder structures.  For example, it doesn't export calendar items (as .ics, for example) or people (as v-cards, for example)
2. Only exports mail messages in .eml (no .msg or bulk export to .pst or .mbox, for example)
3. Doesn't handle e-mail address lookups when the mail format is in Directory Name format
4. Doesn't deal with distribution lists whatsoever
5. Lacking in documentation.  Hopefully the samples provide a good start
6. I'm sure many bugs :) (Also, lacking in test harnesses)
7. Others, I'm sure.  Please let me know in the issues!

## LICENSE
All files that are part of this project are covered by the following
license, except where explicitly noted.

    Version: MPL 1.1/GPL 2.0/LGPL 2.1

    The contents of this file are subject to the Mozilla Public License Version
    1.1 (the "License"); you may not use this file except in compliance with
    the License. You may obtain a copy of the License at
    http://www.mozilla.org/MPL/

    Software distributed under the License is distributed on an "AS IS" basis,
    WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
    for the specific language governing rights and limitations under the
    License.

    The Initial Developer of this software is Shane Connelly (shane@eskibars.com)

    All Rights Reserved.

    Contributor(s):

    Alternatively, the contents of this file may be used under the terms of
    either the GNU General Public License Version 2 or later (the "GPL"), or
    the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
    in which case the provisions of the GPL or the LGPL are applicable instead
    of those above. If you wish to allow use of your version of this file only
    under the terms of either the GPL or the LGPL, and not to allow others to
    use your version of this file under the terms of the MPL, indicate your
    decision by deleting the provisions above and replace them with the notice
    and other provisions required by the GPL or the LGPL. If you do not delete
    the provisions above, a recipient may use your version of this file under
    the terms of any one of the MPL, the GPL or the LGPL.
