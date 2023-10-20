using System;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Morpion
{
    class Program
    {
        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int j, int k)
        {
        	//on vide la console pour l'esthetique
        	Console.Clear();
            for (j=0; j < grille.GetLength(0); j++)
            {
            	//affichage des contours de la grille
                Console.Write("\n|===|===|===|\n");
                Console.Write("|");
                for (k=0; k < grille.GetLength(1); k++)
                {
                	//affichage du contenu de la grille
                	//si c'est le joueur 1 qui s'est approprié la case, on affiche un rond
                	if (grille[j,k] == 1){
                		Console.Write(" O ");
                	}
                	else{
                		//si c'est le joueur 2 qui s'est approprié la case, on affiche une croix
                		if (grille[j,k] == 2){
                			Console.Write(" X ");
                		}
                		//sinon aucun joueur ne s'est encore approprié la case, on affiche un tiret
                		else{
                			Console.Write(" - ");
                		}
                	}
                	//affichage de la derniere colonne du contour de la grille
                    Console.Write("|");
                }
                
            }
            //affichage de la derniere ligne du contour de la grille
            Console.Write("\n|===|===|===|\n");


        }

        // Fonction permettant de changer
        // dans le tableau qu'elle est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int j, int k, int joueur)
        {
        	//tests des valeurs de j et k permettant de ne pas depasser les limites du tableau
        	if (j<0 || j>2 || k<0 ||k>2){
        		//choisi une autre case qui se trouve dans les limite du taleau
        		return true;
        	}
        	else{
        		//si la case n'est pas occuper, le joueur qui joue se l'approprie
	        	if (grille[j,k] == 10) {
	        		grille[j,k] = joueur;
	        		return false;
	        	}
	        	else
	        	{
	        	//sinon il en choisi une autre
	        	return true;
	     
	        	}
        	}
        }

        // Fonction permettant de vérifier
        // si un joueur à gagner
        public static bool Gagner(int l, int c, int joueur)
        {
        	for (l = 0; l < grille.GetLength(0); l++)
        	{
        		for (c = 0; c < grille.GetLength(1); c++) {
		        	//tests de victoire sur les colonnes
		        	//si une colonne est rempli entierement par le meme joueur on renvoie true
		        	if (grille[0,c] == joueur && grille[1,c] == joueur && grille[2,c] == joueur)
		       			{
		      				return true;
						}
					//tests de victoire sur les lignes
					//si une ligne est rempli entierement par le meme joueur on renvoie true
					if (grille[l,0] == joueur && grille[l,1] == joueur && grille[l,2] == joueur)
		        		{
		        			return true;
		       			}
        		}
        	}
        	
        	//tests sur les victoires en diagonale
        	//si une diagonale est rempli entierement par le meme joueur on renvoie true
        	if (grille[0,0] == joueur && grille[1,1] == joueur && grille[2,2] == joueur)
        		{
        			return true;
        		}
        	if (grille[0,2] == joueur && grille[1,1] == joueur && grille[2,0] == joueur)
        		{
        			return true;
        		}
        	//pas de victoire
            return false;
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations ---
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre
            int essais = 0;    // compteur d'essais
	        int joueur = 1 ;   // 1 pour la premier joueur, 2 pour le second
	        int l, c = 0;      // numéro de ligne et de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 
            bool bonnePosition = false; // Permet de vérifier si la position souhaité est disponible

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10
	        for (j=0; j < grille.GetLength(0); j++)
		        for (k=0; k < grille.GetLength(1); k++)
			        grille[j,k] = 10;
	        		//boucle qui se continue tant que gagner vaut false et que le nombre d'essaie est inferieur a 9, soit le nombre de case de la grille
					while(!gagner && essais != 9)
					{
						try
						{
							Console.WriteLine("Ligne   =    ");
							Console.WriteLine("Colonne =    ");
							// Peut changer en fonction de comment vous avez fait votre tableau.
							Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre 
							l = int.Parse(Console.ReadLine()) - 1; 
							// Peut changer en fonction de comment vous avez fait votre tableau.
							Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 10); // Permet de manipuler le curseur dans la fenêtre 
							c = int.Parse(Console.ReadLine()) - 1;
							//mise a jour de la case pour définir quel joueur l'occupe
							
							bonnePosition = AJouer(l,c,joueur);
							//si bonneposition renvoie faux, la partie continue normalement
							if (bonnePosition == false){
								
								//incrementation du compteur d'essais a chaques tours 
								essais++;
								
								//afficher la grille
								AfficherMorpion(l,c);
								
								//verification de victoire
								gagner = Gagner(l,c,joueur);
								//affichage du nom du joueur qui doit jouer
								Console.WriteLine("c'est au tour du joueur "+joueur);
								//changement de joueur a chaques tours
								if (joueur == 1)
								{
									joueur = 2;
								}
								else{
									joueur = 1;
								}
							}
							//si bonne positio renvoie vrai, les conditions pour jouer n'ont pas été respecter, le tour recommence
							else{
								//on vide la console pour l'esthetique
								Console.Clear();
								//on reaffiche la grille
								AfficherMorpion(l,c);
								//on affiche le nom du joueur qui doit jouer
								Console.WriteLine("c'est au tour du joueur "+joueur);
							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e.ToString());
						}
						
						
						
						
						
						
						
					}; // Fin TQ
			//si gagner renvoie vrai, il y a un vainqueur
            if (gagner == true) {
						Console.Clear();
						//affichage du nom du vainqueur
						Console.Write("\nle gagnant est le joueur " + joueur);
            }
			//sinon il ni a pas de vainqueur, donc match nul
			else{
						Console.WriteLine("match nul");
				}
            // A compléter 

            Console.ReadKey();
    }
  }
}