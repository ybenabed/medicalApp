   M S B A M L   `   `   `  ����  WpfApplication1(    "WpfApplication1.PageModifierCompte   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  PageModifierCompte��5      $	��330��6   $	��350��6*   .��5	      � 
�������.+�5
   
   �� -   5        $��Recstackmodifcpt��5
      $��Center=�6,   $��Center=�6I   $	��350��6d   $	��330��6p   
�������$y�Visible=�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5      �� 5   
    �� Orientation$ Vertical=�6   .+�5      �� $ 
Horizontal=�6   $��0,30,0,0q�62   .+�5      �� $��
Username :��6   $��15(�6-   �� 5      -   5        $��Username��5      $	��150��6*   $
��10,0q�66    �� 	MaxLength$ 20��6~   5      �� 5      -   5        $��stack��5   +   $��0,15,0,0q�6   .+�5      �� -   5        $��ChangePassword��5      $��Modifier mot de passe��61   $��13(�6Q   	 d� 	IsChecked$	 FalseY�6|   5      �� 5      -   5        $��stack2��5   2   $ 
Horizontal=�6   $y�	Collapsed=�6@   $��0,15,0,0q�6W   .+�5      �� $��Password  :��6   $��15(�6.   G� 5      -   5        $��Password��5      $	��150��6.   $��10,0,0,0q�6:   
 G� 	MaxLength$
 20��6L   6v   �� 5      -   5        $��showPassword��5      $	��150��6.   $��
-150,0,0,0q�6:   $y�Hidden=�6N   5      �� 5      $ 
Horizontal=�6   $��0,15,0,0q�62   .+�5      �� $��Spécialité  :��6   $��15(�60   �� 5      -   5        $��Spécialité��5      $	��150��6,   $
��10,0q�68   5      �� 5       $��0,15,0,0q�6   .+�5!      �� $��Type Compte :��6   $��15(�60   5"      �� 5#      $ 
Horizontal=�6   $��25��62   $��0,15,0,0q�6>   .+�5$      	� -	   5        $��Medadmin��5$      $��MedcinAdmin��6.   $
��50,0q�6D   $��13(�6R   	� 5%      -
   5        $	��Med��5%      $��Medcin��6)   $
��10,0q�6:   $��13(�6H   5&      �� 5'      $��40��6   $ 
Horizontal=�6%   $��0,25,0,0q�6>   .+�5(      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        $��
valider_ic��5(   *   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ CheckCircle=�6<   $��30��6O   $��30��6[   $��	230,0,0,0q�6f    � Cursor$
 Hand��6y   
�������6�     5)      -   5        $��	retour_ic��5)   *   $ CloseCircle=�6;   $��30��6N   $��30��6Z   $��30,0,0,0q�6e   $
 Hand��6w   
�������6�   5*      �� 5+      $ 
Horizontal=�6   5,      5-      �� 5.   
   -   5        $��224,85,106,226q�5.      .+�5/        -   5        $��eye_password��5/   &   $	 Eye=�6:   50        51   
   -   5        $��ic_close��51   "   $ Close=�62   $��20��6?   $��20��6K   $��	330,0,0,0q�6V   $
 Hand��6�   
�������6�   �� 52   
   -   5        $��	valid_box��52      $��Valider��6!   $��15(�63   $��219,283,75,0q�6A   
�������$y�Hidden=�6j   $��32��6~   �� 53   
   -   5        $��	annul_box��53      $��Annuler��6!   $��15(�63   $��280,283,8,0q�6A   
�������$y�Hidden=�6i   $��32��6}   54      55      