   M S B A M L   `   `   `  ����E  AGymManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GymManager
GymManager  5      !    GymManager.showPaymentTypes   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:GymManager     5        N� Title$  showPaymentTypes��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��paymentTypesList��5         AutoGenerateColumns .  $
��Left=�6G   $	��649��6b   $	��Top=�6o   $
��1108��6�      
IsReadOnly . $��0,20,0,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa��5   !   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5   %   �� *Name+   HeaderTemplate 5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6)   .+�5   "   �� ��6,   �� *	Content+ �� RelativeSource �� 	 �� Mode$	 TemplatedParent=��� 5   "   
 ��Name$
 txtName��6*   -   5        $	��100��5   ;   6G   5      5      5      5      �� 5   
   -   5        $
��Nowy��5   )   $
��Left=�68   $	��Top=�6S   $��75��6k   $��	304,0,0,0q�6v   �� 5   
   -   5        $
��text��5      $
��Left=�6    $��80,0,0,0q�6;   $
��Wrap=�6M   $��TextBlo��6a   $	��Top=�6p   $��20��6�   �� 5   
   $
��Left=�6   $��10,0,0,0q�6/   $
��Wrap=�6A   $��Szukaj: ��6U   $	��Top=�6e   $��20��6}   �� 5   
   -   5        $
��Left=�5      $��20��6-   $��54,0,0,0q�69    �� TextWrapping$
 Wrap=�6K   $�� ��6_   $	��Top=�6g   $	��120��6   6�   �� 5    
   -   5        $��whatToSearch��5       $
��Left=�6'   $��	179,0,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5!      �� $��Nazwa��6   $
��Left=�6+   $	��118��6F    �� 
IsSelected . 6R   5"      5$      5%      