   M S B A M L   `   `   `  ����  WpfApplication1$    WpfApplication1.GestionComptes   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  GestionComptes��5   	   $	��600��6    $	��800��6-    � 
ResizeMode$ NoResize=�69    � WindowStartupLocation$ CenterScreen=�6O    � WindowStyle$
 None=�6t   .��5      �� 
�������$
��Left=�6$   $	��800��6?   .+�5   
   � -   5        $��maingrd��5      $��40��6   $
��Left=�6*   $	��800��6E   ���#0c4a60.+�5	      �� $
��Left=�6   $��0q�64   �� 5
       �� Orientation$ 
Horizontal=�6   $��Right=�62   $��	5,0,750,0q�6N   .+�5      �� -   5        $��	shut_butt��5      $��Center=�6W   ��6*   Z� ��6@   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        $��shutdown��5   .   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Logout=�6>   ���	#FFA2A2A2$��25��6c   $��25��6n    � Cursor$
 Hand��6z    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5   "   �� �� 5   "   �� 5   "    �� Angle$	 180Q�62   T� 5   "   5      5      5      5      5      � 5   
   $	��100��6   $
��Left=�6   $	��800��67   .+�5      �� $ 
Horizontal=�6   $	��Top=�62   $��10,0,0,0q�6J   .+�5      �� -   5        
	 J� Uid$	 0��5      $	��150��6!   $��COMPTES��6-   $��50��6?   ���#0c4a60��6K   Z� ��6a   Z� 6�   �� 5      -   5        $	 1��5      $	��150��6!   $��NOUVEAU��6-   $��50��6?   ���#0c4a60��6K   Z� ��6a   Z� 6�   5      � 5      -   5        
 �Name$
 
GridCursor��5      $	��150��6'   $��5��63   ���	#FFD54A42$
��Left=�6U   $
��10 0q�6p   5      � 5    
   -   5        $
 GridMain��5       $	��460��6!   
�������.+�5!       � -   5        $��Gestion_support��5!      $
��Left=�6+   $	��460��6F   $	��Top=�6S   $	��800��6k     � NavigationUIVisibility$ Hidden=�6w   5"      �� 5#   
   -	   5         ��Name$ inactivityMessage��5$   
   $��70��5#      $	��350��6"   ���	#FFF30000$y�Hidden=�6E   $��Center=�5$   %   $��0,-265,0,500q�6B   .+�5%      �� $ 
Horizontal=�6   $��Center=�62   .+�5&      �� $,��&Votre session se déconnectera aprés ��6   $��14(�6G   
�������$��SemiBold"�6h   �� 5'      -
   5         ��Name$ minuteur��5'      $��14(�6*   
�������$��SemiBold"�6K   $��-8,0,0,0q�6a   �� 5(      $��
seconde(s)��6   $��14(�6-   
�������$��SemiBold"�6N   $��-8,0,0,0q�6d   5)      �� 5*      -   5         ��Name$ annuler��5*   '   $��Annuler��6   $��0,3,0,0q�68   $��90��6I   ��6j   Z� ��6�   Z� 5+      5,      5-      