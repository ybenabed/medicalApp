   M S B A M L   `   `   `  ����  WpfApplication1'    !WpfApplication1.PageGestionCompte   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��800��5   0   $	��460��6<     N� Title$  PageGestionCompte��5	      .��5
      � .+�5   
   �� -   5        $��Recconfirmstack��5      $��Center=�6+   $��Center=�6H   $	��800��6c   $	��460��6o   
�������$y�Visible=�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5      � 5   
   -   5        $��Grdcpt��5   '   
�������.+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�62   $��40��6J   $��0,30,0,0q�6V   .+�5      �� $��Nom :��6   
���   �$��14(�6;   $��Center=�6I   $��30,0,0,0q�6d   �� 5      -   5        $��med_nom��5      $	��150��6)   
���   �$��Center=�6I   6d   �� 5      $��Prenom :��6   
���   �$��14(�6>   $��Center=�6L   �� 5      -   5        $��
med_prenom��5      $	��150��6,   
���   �$��Center=�6L   6g   5      �� 5      $��	0,100,0,0q�6   $	��300��6,   $	��Top=�69   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��	DatagGrid��5   L   $��0,0,0,0q�6   $	��300��6,   
�������   
IsReadOnly . 	   Columns	 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      
   CellTemplate
 5      �� 5   "   .R�5   &   �� $��btnGrid��6K   -   5        ���	#FFD54A42
����������	#FFD54A42��5   -   ��  �� Path$
 Etat�6�   5   #   5       5"      5#      �� 5$      $��40��6   $��
0,410,0,10q�6%   .+�5%      �� -   5        $��
butt_modif��5%      $��Modifier��6+   $��Right=�6>   $��0,5,20,0q�6Z   ���	#FFD54A42��6�   Z� ��6�   Z� 5&       � 5'      -   5        $��page_modifier_support��5'        � NavigationUIVisibility$ Hidden=�61   5(      5)      �� 5*   
   -	   5        $��RecStackcptselcet��5*      $��Center=�6-   $��Center=�6J   $	��208��6e   $��60��6q   $��296,200q�6}   
�������$y�Hidden=�6�    5+        5,      $ 15Q�6#   $ 0Q�63   $ Quality=�6A   $ 1Q�6Y   $ 	#FFBBBBBB��6i   5.      �� 5/   
   -
   5        $��Stackcptselcet��5/      $ Vertical=�6+   $��Center=�6B   $��Center=�6_   $y�Hidden=�6z   .+�50      �� $&�� Aucun compte n'est sélectionné��6   $��14(�6=   �� 51      $��Center=�6   .+�52      �� -   5        $��	buttokcpt��52      $��ok��6*   ���	#FFD54A42��67   Z� ��6M   Z� 6{   53      54      55      56      