   M S B A M L   `   `   `  ����  WpfApplication1%    WpfApplication1.Page_Ordonnance   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��590��5      $
��1080��6   .��5	      �� 
�������$��0,0,0,0q�6)   .+�5
   
   � $��10��6   5      � 5   
   -   5        $��	GridBarre��5      $��60��6   $y�	Collapsed=�6,   .+�5      ��   �� Orientation$  
Horizontal=�6   $	��Top=�62   $
��10 0q�6J   5      � 5   
   -   5         �Name$ GridMain��5      $��	0,-10,0,0q�6!   $	��520��64   
�������.+�5      �� -   5        $��
Affichage2��5      $��	400,0,0,0q�6+   $y�Hidden=�6>   .+�5      �� $	��490��6   $	��560��6&   $
��60,0q�62   ._�5      � 
�������$��0,10,0,0q�6.   .+�5      �� 
�������$	��560��6=   $��0,0,0,0q�6I   .+�5      �� $��Nouvelle ordonnance��6$   $��20(�6B   $��Center=�6P   �� 5      $  
Horizontal=�6)   $��	0,0,315,0q�6B   .+�5   "   �� $��Nom :��6(   $��15(�68   $��10,0,0,0q�6F   �� 5   "   -   5        $��	Nom_Medic��5   +   $	��150��6<   $��23,0,0,0q�6�   5      �� 5      $  
Horizontal=�6)   $��	0,5,315,0q�6B   .+�5   "   �� $��Format :��6(   $��15(�6;   $��10,0,0,0q�6I   �� 5   "   -   5        $��Forme_Medic��5   +   $	��150��6>   $��10,0,0,0q�6J   .@�5   &   �� $��	Comprimé��63   �� 5   &   $��Gélule��63   �� 5   &   $��Sirop��63   �� 5   &   $��Gouttes��63   �� 5    &   $��	Injection��63   �� 5!   &   $��Suppositoire��63   �� 5"   &   $��Suspension buvable��63   �� 5#   &   $��Pommade��63   �� 5$   &   $��Crème��63   �� 5%   &   $	��Gel��63   �� 5&   &   $��Patch��63   5'   #   5(      �� 5)      $  
Horizontal=�6)   $��	0,5,315,0q�6B   .+�5*   "   �� $��Dose :��6(   $��15(�69   $��10,0,0,0q�6G   �� 5+   "   -   5        $��
Dose_Medic��5+   *   $	��150��6<   $��22,0,0,0q�6H   5,      �� 5-      $  
Horizontal=�6)   $��	0,5,315,0q�6B   .+�5.   "   �� $��Nombre de boites :��6(   $��15(�6E   $��10,0,0,0q�6S   �� 5/   "   -   5         ��Name$ Quant_Medic��5/   +   $��80��6@   $��5,0,0,0q�6K   .@�50   &   �� $��1��63   �� 51   &   $��2��63   �� 52   &   $��3��63   �� 53   &   $��4��63   �� 54   &   $��5��63   �� 55   &   $��6��63   �� 56   &   $��7��63   �� 57   &   $��8��63   �� 58   &   $��9��63   �� 59   &   $��10��63   5:   #   5;      �� 5<      $  
Horizontal=�6)   $��	0,5,315,0q�6B   .+�5=   "   �� $��Durée :��6(   $��15(�6:   $��10,0,0,0q�6H   �� 5>   "   -   5        $
 Dure��5>   +   $	��150��69   $��15,0,0,0q�6E   .@�5?   &   �� $
��Jour��63   �� 5@   &   $��2 Jours��63   �� 5A   &   $��3 Jours��63   �� 5B   &   $��4 Jours��63   �� 5C   &   $��5 Jours��63   �� 5D   &   $��6 Jours��63   �� 5E   &   $��	1 Semaine��63   �� 5F   &   $��10 Jours��63   �� 5G   &   $��
2 Semaines��63   �� 5H   &   $��
3 Semaines��63   �� 5I   &   $��
4 Semaines��63   5J   #   5K      �� 5L      $  
Horizontal=�6)   $��0,5,0,0q�6B   .+�5M   "   �� $��Nombre de prise :��6(   $��15(�6D   $��10,0,0,0q�6R   �� 5N   "   -	   5        $��	Nbre_fois��5N   +   $��80��6<   $��15,0,0,0q�6G   .@�5O   &   �� $��1 fois��63   �� 5P   &   $��2 fois��63   �� 5Q   &   $��3 fois��63   �� 5R   &   $��4 fois��63   �� 5S   &   $��5 fois��63   �� 5T   &   $��6 fois��63   �� 5U   &   $��7 fois��63   �� 5V   &   $��8 fois��63   �� 5W   &   $��9 fois��63   �� 5X   &   $��10 fois��63   5Y   #   �� 5Z   "   $��Chaque :��6(   $��15(�6;   $��10,0,0,0q�6I   �� 5[   "   -
   5        $��	frequence��5[   +   $	��120��6<   .@�5\   &   �� $
��Jour��63   �� 5]   &   $��2 Jours��63   �� 5^   &   $��3 Jours��63   �� 5_   &   $��4 Jours��63   �� 5`   &   $��5 Jours��63   �� 5a   &   $��6 Jours��63   �� 5b   &   $��	1 Semaine��63   �� 5c   &   $��10 Jours��63   �� 5d   &   $��
2 Semaines��63   �� 5e   &   $��
3 Semaines��63   �� 5f   &   $��
4 Semaines��63   5g   #   5h      �� 5i      $��0,10,0,0q�6)   $��30��6;   $  
Horizontal=�6G   .+�5j   "   �� -   5         ��Name$ Etat_dajout��5j   (   $
��Fill=�6=   $��10,0,0,0q�6M   $y�Visible=�6_   $��20��6t   $��20��6   �� 5k   "   -   5        $��Etat_dajoutTxt��5k   (   $	��200��6>   $y�Visible=�6J   $��10,0,0,0q�6_   $
��Left=�6q   �� 5l   "   -   5        $��	addButton��5l   <   $��Ajouter��6)   $��	130,0,0,0q�6j   $��Center=�6}   $��30��6�   ���	#FFD54A42���	#FFD54A426�   �� 5m   "   -   5        $��updateButton��5m   )   $��Modifier��6[   $��Center=�6n   $��10,0,0,0q�6�   $��30��6�   $��90��6�   ���	#FFD54A42���	#FFD54A426�   5o      &   System.Windows.Controls.DataGrid  5p      -   5          Name$ Affich_Medic_Ajouté��5p   '      AlternatingRowBackground
 �����   CanUserDeleteRows .     CanUserAddRows .     
IsReadOnly . 	   AutoGenerateColumns	 . $��0,5,0,5q�5q   k   $	��160��5r   '   
   RowBackground#
 � ��6l    �� AlternationCount$ 2��5s   '   }�. $��11(�6P   
�������   Columns 5t   "   4  .System.Windows.Controls.DataGridTemplateColumn  5u   &      CellTemplate 5v   *   �� 5w   .   .R�5x   2   '  !MaterialDesignThemes.Wpf.PackIcon  -   5        �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Delete=�5x   J   
���  �� � Cursor$
 Hand��6i   
�������6�   5y   /   5{   '   5}      �� 5~      $  
Horizontal=�6)   $��40��6B   .+�5   "   �� -   5        $��Terminer��5   )   $��90��69   $��Terminer��6D   $��	370,0,0,0q�6n   ���	#FFD54A42��6�   Z� ��6�   Z� 6�   �� 5�   "   -   5         ��Name$ Retour��5�   )   $��95��69   $��Imprimer��6D   $��5,5,0,5q�6n   $��Center=�6   $��30��6�   ���	#FFD54A42��6�   Z� ��6�   Z� 6�   5�      5�       � 5�      -   5        $��	Confirmer��5�       $��80��62   $��Center=�6>   $��Center=�6Y   $��120,215,160,215q�6v   $	��280��6�   �� 5�      -   5        $��Table_stack��5�   %   $	��Top=�68   $
��Left=�6P   $	��165��6k   $	��280��6x   $��
270,45,0,0q�6�   .+�5�        -   5        $��datag_medic��5�   9    . $	��165��6L   $��0,0,0,0q�6Y   6j   5�      5�      5�      5�      � 5�      -   5        $��Visualisation��5�      $��	430,0,0,0q�6(   .+�5�      Z� -   5        $��Viewer��5�   6   $��10,30,10,10q�6!   66   �� 5�      -   5        $��Close_��5�   F   $��595,0,0,435q�6i   ��6   Z� ��6/   Z� .��5�        $ CloseCircle=�6.   ���	#FFA2A2A2$��25��6X   $��25��6c    J� RenderTransformOrigin$ 0.5,0.5&�6o   5�      5�      5�      �� 5�      -   5        $��AffichageStack��5�      .+�5�      �� $  
Horizontal=�6   $��40��66   $	��Top=�6B   .+�5�      �� -   5         ��Name$ 	LabelMois��5�      $��Mois  ��60   $��13(�6A   $��Bottom=�6O   $��60,0,0,0q�6j   �� 5�      -   5        $
 Mois��5�      $��60��6-   $��Bottom=�68   6S   �� 5�      -   5        $ 
LabelAnnee��5�      $��	   Année��60   $��13(�6C   $��Bottom=�6Q   �� 5�      -   5        $ Annee��5�      $��80��6.   $��Bottom=�69   6T   5�        5�      -   5        $ 	DatagGrid��5�   4   $��25,10,660,0q�6G    . $	��400��6n    5�        5�       5�      �� 5�   "   .R�5�   &   �� $  
Horizontal=�61   .+�5�   *   �� $ btnGrid��6L   -   5        $��Ouvrir avec Word��5�   1   ���	#FFD54A42���	#FFD54A42
�������6�   '  !MaterialDesignThemes.Wpf.PopupBox  5�   *      	StaysOpen .     PlacementMode$ RightAndAlignMiddles=�6T      PopupContent. 5�   .   �� .+�5�   2   �� $
 ouvr��6V   -    5        $��Ouvrir dans CureIt��5�   9   6d   �� 5�   2   $��openDirectory��6X   -!   5        $��Ouvrir l'emplacement��5�   9   6m   5�   /   5�   +   5�   '   5�   #   5�      5�      �� 5�      $  
Horizontal=�6   $��0,5,0,0q�66   .+�5�      �� -"   5        $ 	Supprimer��5�      $��	Supprimer��60   $	��100��6D   $��
320,10,0,0q�6P   {�.  ���	#FFD54A42���	#FFD54A42
�������6�   5�      5�      5�      5�      5�      