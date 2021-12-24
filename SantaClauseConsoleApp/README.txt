About app

Start and run:
To run the application open the SantaClauseConsoleApp.sln and hit run, it starts in the document "Program.cs"
In the folder Constants we have the project relative path to each storage file for: childs, letters, respective their content.

Diagram:
In the Diagram folder theres a StarUML project and a photo of the diagram.

IMPORTANT MENTION
Adress should be like this in : child file -> [City, Town, Street] [City, Town.]
								letter content -> [Jud. City, Loc. Town, Street] [Jud. City, Loc. Town.]
Ex: 
Child file:
--------------------------------------------------
|"Botosani, Botosani, Strada asdasd nr.1232312." |
|"Cluj, Cluj-Napoca."                            |
--------------------------------------------------

Letter file:
------------------------------------------------------------
|"Jud. Botosani, Loc. Botosani, Strada asdasd nr.1232312." |
|"Jud. Cluj, Loc. Cluj-Napoca."                            |
------------------------------------------------------------

Add/delete letter files:
To create letter files, we have to create a letter entity using the service, repository or by simply creating a letter file respecting
the letter format. The letters are independent
from the childs in repository but the service will take into consideration all the logic to create a letter.

Add/delete child:
Simply create and save one by using the service/repo or add it directly into the file. 
All other information can be found in the comments and through the code, hopefully I covered everything.




Looking forward to hearing from you :)