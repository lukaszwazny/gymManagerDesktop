   M S B A M L   `   `   `  ����E  AGymManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GymManager
GymManager  5          GymManager.packageHistory   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GymManager     5        N� Title$  packageHistory��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��packagesList��5         AutoGenerateColumns .  $
��Left=�6C   $	��649��6^   $	��Top=�6k   $
��1108��6�      
IsReadOnly . $��0,20,0,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa��5   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *Name+   HeaderTemplate 5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+ �� RelativeSource �� 	 �� Mode$	 TemplatedParent=��� 5   "   
 ��Name$
 txtName��6*   -   5        $	��100��5   ;   6G   5      5      5        5      $
 Cena��5   !    5   %   �� *Price+ 5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 5!   "   $
 txtPrice��6*   -   5        $	��100��5!   <   6H   5"      5#      5%        5&      $ Czas trwania��5'   !    5&   %   �� *	TimeLimit+ 5(      �� 5)      .R�5*      �� $ 
Horizontal=�6)   .+�5+   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 5-   "   $
 txtTimeLimit��6*   -   5        $	��100��5-   @   6L   5.      5/      51        52      $ Limit wejść��53   !    52   %   �� *EntrancesLimit+ 54      �� 55      .R�56      �� $ 
Horizontal=�6)   .+�57   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 59   "   $
 txtEntrancesLimit��6*   -   5        $	��100��59   E   6Q   5:      5;      5=        5>      -   5        PurchaseDate��$ Data zakupu��5?   !    5>   ;   �� *PurchaseDate+ �� StringFormat$ {0:dd.MM.yyyy}�� 5@      �� 5A      .R�5B      �� $ 
Horizontal=�6)   .+�5C   "   �� ��6,   �� *	Content+ �� $	 TemplatedParent=��� 5E   "   $
 txtPurchaseDate��6*   -   5        $	��100��5E   C   6O   5F      5G      5I      5K      �� 5L   
   -   5        $
��Nowy��5L   %   $
��Left=�64   $	��Top=�6O   $��75��6g   $��	304,0,0,0q�6r   �� 5M   
   -	   5        $
��text��5M      $
��Left=�6    $��80,0,0,0q�6;   $
��Wrap=�6M   $��TextBlo��6a   $	��Top=�6p   $��20��6�   �� 5N   
   $
��Left=�6   $��10,0,0,0q�6/   $
��Wrap=�6A   $��Szukaj: ��6U   $	��Top=�6e   $��20��6}   �� 5O   
   -
   5        $
��Left=�5O      $��20��6-   $��54,0,0,0q�69    �� TextWrapping$
 Wrap=�6K   $�� ��6_   $	��Top=�6g   $	��120��6   6�   �� 5P   
   -   5        $��whatToSearch��5P      $
��Left=�6'   $��	179,0,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5Q      �� $��Nazwa��6   $
��Left=�6+   $	��118��6F    �� 
IsSelected . 6R   �� 5R      $
��Cena��6   $
��Left=�6*   $	��118��6E   �� 5S      $��Czas trwania��6   $
��Left=�62   $	��118��6M   �� 5T      $��Limt wejść��6   $
��Left=�60   $	��118��6K   �� 5U      $��Data zakupu��6   $
��Left=�61   $	��118��6L   5V      5X      5Y      