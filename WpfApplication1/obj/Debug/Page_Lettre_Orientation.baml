   M S B A M L   `   `   `  ����  WpfApplication1-    'WpfApplication1.Page_Lettre_Orientation   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��590��5      $
��1080��6   .��5	      �� 
�������.+�5
   
   � $��10��6   5      � 5   
   -   5        $��	GridBarre��5      $��60��6   $y�	Collapsed=�6,   .+�5      ��   �� Orientation$  
Horizontal=�6   $	��Top=�62   $
��10 0q�6J   � 5      -   5         �Name$ 
GridCursor��5      $	��200��6'   $��5��63   ���	#FFD54A42$
��Left=�6U   $��
10,45,0,10q�6p   5      � 5   
   -   5        $ GridMain��5      $	��520��6!   
�������.+�5      � -   5        $��Visualisation��5      $��
440,0,0,10q�6(   $y�Hidden=�6<   .+�5      (  "System.Windows.Controls.WebBrowser  -   5        $��	pdfViewer��5      $��30,30,10,10q�6.   �� 5      -   5        $��Close_��5   F   $��585,0,0,477q�6i   ��6   Z� ��6/   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ CloseBox=�6.   ���	#FFA2A2A2$��25��6U   $��25��6`    J� RenderTransformOrigin$ 0.5,0.5&�6l   5      5      5      �� 5      -   5        $��
Affichage2��5      $��480,10,0,10q�6+   .+�5      �� $	��375��6   $��0,10,0,0q�6&   
���   �._�5      � .+�5      � -   5        $��	grdlettre��5   E   
�������$	��420��68   .+�5      �� $  Vertical=�6)   $��10,10,10,33q�6@   .+�5   "   �� $��Nouvelle lettre��6(   $��20(�6B   $��Center=�6P   �� 5   "   $  
Horizontal=�6-   $��0,0,0,0q�6F   $��Bottom=�6X   .+�5   &   �� $��Au médecin :��6,   $	��110��6C   $��0,10,0,0q�6O   $��15(�6a   �� 5    &   -	   5         ��Name$ nom_med��5    .    �� 	MaxLength$ 50��6?   $	��250��6N   $��13(�6Z   5!   #   �� 5"   "   $  
Horizontal=�6-   $��0,0,0,0q�6F   $��Bottom=�6X   .+�5#   &   �� $��Specialiste en :��6,   $	��110��6H   $��0,10,0,0q�6T   $��15(�6f   �� 5$   &   -
   5        $
��Spec��5$   .   $	��250��6:   $ 20��6F   $��13(�6V   5%   #   �� 5&   "   $  
Horizontal=�6-   $��0,0,0,0q�6F   $��Bottom=�6W   .+�5'   &   �� $��	Adresse :��6,   $	��110��6@   $��0,10,0,0q�6L   $��15(�6^   �� 5(   &   -   5        $	��adr��5(   .   $	��250��69   $ 80��6E   $��13(�6T   5)   #   �� 5*   "   $  Vertical=�6-   $��0,0,0,0q�6D   $	��180��6U   $��Bottom=�6b   .+�5+   &   �� $��	Rapport :��6,   $��
0,5,510,10q�6@   $��15(�6T   �� 5,   &   -   5        $	 com��5,   /   $	��120��6<    �� AcceptsReturn . $
 1000��6^   $��13(�6o   $��	28,0,96,0q�6}   $	��Top=�6�   5-   #   �� 5.   "   $  
Horizontal=�6-   $��	0,-12,0,0q�6F   $��Bottom=�6Y   .+�5/   &   �� -   5        $��Enregistrer��5/   -   $��14(�6C   $��
360,10,0,8q�6Q   $	��188��6{   ���	#FFD54A42��6�   Z� ��6�   Z� 6�   50   #   51      52      �� 53      -   5        $��RecStacknonremplett��53   $   $��Center=�6?   $��Center=�6\   $	��230��6w   $��61��6�   $��210,157q�6�   
�������$y�Hidden=�6�    J� Effect 54      3  -System.Windows.Media.Effects.DropShadowEffect  55   "      
BlurRadius$ 15Q�63   	   	Direction$	 0Q�6C   
   RenderingBias$
 Quality=�6Q      ShadowDepth$ 1Q�6i      Color$ 	#FFBBBBBB��6y   57      �� 58      -   5        $��Stacknonremplett��58   %   $  Vertical=�6=   $��Center=�6T   $��Center=�6q   $y�Hidden=�6�   .+�59      �� $+��%Verifiez la saisie de tous les champs��6$   $��14(�6T   �� 5:      $��Center=�6)   .+�5;   "   �� -   5        $��buttoklettre��5;   6   $��ok��6)   ���	#FFD54A42��6a   Z� ��6x   Z� 6�   5<      5=      �� 5>      -   5        $��RecStackbienajouté��5>   $   $��Center=�6>   $��Center=�6[   $	��230��6v   $��61��6�   $��210,157q�6�   
�������$y�Hidden=�6�    5?        5@   "   $ 15Q�63   $	 0Q�6C   $
 Quality=�6Q   $ 1Q�6i   $ 	#FFBBBBBB��6y   5B      �� 5C      -   5        $��Stackbienajouté��5C   %   $  Vertical=�6<   $��Center=�6S   $��Center=�6p   $y�Hidden=�6�   .+�5D      �� $ ��La lettre est enregistrée��6$   $��14(�6H   �� 5E      $��Center=�6)   .+�5F   "   �� -   5        $��buttokenreg��5F   )   $��ok��6<   ���	#FFD54A42��6`   Z� ��6v   Z� 6�   5G      5H      5I      5J      5K      �� 5L      -   5        $��AffichageStack��5L      $y�Visible=�6/   .+�5M      �� $  
Horizontal=�6   $��10��66   $	��Top=�6B   &   System.Windows.Controls.DataGrid  5N      -   5        $��	DatagGrid��5N   -      
IsReadOnly . $��25,22,650,0q�6>   $	��430��6S      Columns 5O      4  .System.Windows.Controls.DataGridTemplateColumn  5P         CellTemplate 5Q      �� 5R   "   .R�5S   &   �� $  
Horizontal=�61   .+�5T   *   �� $��btnGrid��6B   -   5        $��Ouvrir��5T   1   ���	#FFD54A42���	#FFD54A42
�������6�   '  !MaterialDesignThemes.Wpf.PopupBox  5U   *      	StaysOpen .     PlacementMode$ RightAndAlignMiddles=�6T      PopupContent. 5V   .   �� .+�5W   2   ��  ��Name$
 ouvr��6V   -   5        $��Ouvrir dans CureIt��5W   9   6d   �� 5X   2   $��openDirectory��6X   -   5        $��Ouvrir l'emplacement��5X   9   6m   5Y   /   5Z   +   5[   '   5\   #   5^         RowDetailsTemplate 5`      �� 5a      .R�5b      �� $��1q�6%   ���	#FF2196F3._�5c   "   �� .+�5d   &   �� $  
Horizontal=�61   .+�5e   *   �� $��13(�64   $��Commentaire: ��6B   $��SemiBold"�6W   �� 5f   *   $��13(�64   ��6B   �� *cmnt+5g   '   �� 5h   &   $  
Horizontal=�61   .+�5i   *   �� $��13(�64   $��	Adresse: ��6B   $��SemiBold"�6S   �� 5j   *   $��13(�64   ��6B   �� *adr+5k   '   5l   #   5m      5n      5p      �� 5q      $  
Horizontal=�6   .+�5r      �� -   5        $��	Supprimer��5r      $��	Supprimer��6.   $	��100��6B   $��
327,15,0,0q�6N   {�.  
����������	#FFD54A42���	#FFD54A426�   5s      5t      5u      5v      5w      