using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour
{

    public SpriteRenderer ShapeSprite;
    public Sprite[] Shape;
    public GameObject MergeBoosterEffect;


    [Header("Parent")]
    public itemGrid board;
    public Node node;

    [Header("Variables")]
    public int color;
    public int destroycolor;
    public ITEM_TYPE type;
    public ITEM_TYPE next = ITEM_TYPE.NONE;
    public BREAKER_EFFECT effect = BREAKER_EFFECT.NORMAL;

    [Header("Check")]
    public bool drag;
    public bool nextSound = true;
    public bool destroying;
    public bool dropping;
    public bool changing;
    public bool changedSprite;
    public Vector3 mousePostion = Vector3.zero;
    public Vector3 deltaPosition = Vector3.zero;
    public Vector3 swapDirection = Vector3.zero;

    [Header("Swap")]
    public Node neighborNodeLeft;
    public Node neighborNodeRight;
    public Node neighborNodeTop;
    public Node neighborNodeBottom;
    public Node neighborNodeTopLeft;
    public Node neighborNodeTopRight;
    public Node neighborNodeBottomLeft;
    public Node neighborNodeBottomRight;


    public Node neighborNode;
    public Item swapItem;
    public Item KomsuItems;

    [Header("Komsu Items")]
    public Item komsuNodeLeft;
    public Item komsuNodeRight;
    public Item komsuNodeTop;
    public Item komsuNodeBottom;
    public Item komsuNodeTopLeft;
    public Item komsuNodeTopRight;
    public Item komsuNodeBottomLeft;
    public Item komsuNodeBottomRight;


    [Header("Drop")]
    public List<Vector3> dropPath;

    public void Start()
    {
        CheckStat();
    }

    public void CheckStat()
    {
        if (node.LeftNeighbor() != null)
        {
            if (node.LeftNeighbor().item != null)
            {
                neighborNodeLeft = node.LeftNeighbor();
                if (IsBooster())
                    komsuNodeLeft = node.LeftNeighbor().item;
            }
        }

        if (node.RightNeighbor() != null)
        {
            if (node.RightNeighbor().item != null)
            {
                neighborNodeRight = node.RightNeighbor();
                if (IsBooster())
                    komsuNodeRight = node.RightNeighbor().item;

            }
        }

        if (node.TopNeighbor() != null)
        {
            if (node.TopNeighbor().item != null)
            {
                neighborNodeTop = node.TopNeighbor();
                if (IsBooster())
                    komsuNodeTop = node.TopNeighbor().item;

            }
        }

        if (node.BottomNeighbor() != null)
        {
            if (node.BottomNeighbor().item != null)
            {
                neighborNodeBottom = node.BottomNeighbor();
                if (IsBooster())
                    komsuNodeBottom = node.BottomNeighbor().item;

            }
        }
        if (node.TopLeftNeighbor() != null)
        {
            if (node.TopLeftNeighbor().item != null)
            {
                neighborNodeTopLeft = node.TopLeftNeighbor();
                if (IsBooster())
                    komsuNodeTopLeft = node.TopLeftNeighbor().item;

            }
        }

        if (node.TopRightNeighbor() != null)
        {
            if (node.TopRightNeighbor().item != null)
            {
                neighborNodeTopRight = node.TopRightNeighbor();
                if (IsBooster())
                    komsuNodeTopRight = node.TopRightNeighbor().item;
            }
        }

        if (node.BottomLeftNeighbor() != null)
        {
            if (node.BottomLeftNeighbor().item != null)
            {
                neighborNodeBottomLeft = node.BottomLeftNeighbor();
                if (IsBooster())
                    komsuNodeBottomLeft = node.BottomLeftNeighbor().item;

            }
        }

        if (node.BottomRightNeighbor() != null)
        {
            if (node.BottomRightNeighbor().item != null)
            {
                neighborNodeBottomRight = node.BottomRightNeighbor();
                if (IsBooster())
                    komsuNodeBottomRight = node.BottomRightNeighbor().item;

            }
        }


        if (IsCookie())
        {
            getChangeSprite();
        }
        if (IsBooster())
        {
            getChangeSpriteBoosters();
        }
    }
    void Update()
    {
        checkNeighbor();
        if (drag)
        {
            getNeighbor();
        }
    }




    #region Type


    public bool Movable()
    {
        if (type == ITEM_TYPE.MINE_1_LAYER ||
            type == ITEM_TYPE.MINE_2_LAYER ||
            type == ITEM_TYPE.MINE_3_LAYER ||
            type == ITEM_TYPE.MINE_4_LAYER ||
            type == ITEM_TYPE.MINE_5_LAYER ||
            type == ITEM_TYPE.MINE_6_LAYER ||
            type == ITEM_TYPE.ROCK_CANDY_1 ||
            type == ITEM_TYPE.ROCK_CANDY_2 ||
            type == ITEM_TYPE.ROCK_CANDY_3 ||
            type == ITEM_TYPE.ROCK_CANDY_4 ||
            type == ITEM_TYPE.ROCK_CANDY_5 ||
            type == ITEM_TYPE.ROCK_CANDY_6)
        {
            return false;
        }

        // cage
        if (node.cage != null)
        {
            if (node.cage.type == LOCK_TYPE.LOCK_1)
            {
                return false;
            }
        }

        return true;
    }

    public bool Matchable()
    {
        if (type == ITEM_TYPE.MINE_1_LAYER ||
            type == ITEM_TYPE.MINE_2_LAYER ||
            type == ITEM_TYPE.MINE_3_LAYER ||
            type == ITEM_TYPE.MINE_4_LAYER ||
            type == ITEM_TYPE.MINE_5_LAYER ||
            type == ITEM_TYPE.MINE_6_LAYER ||
            type == ITEM_TYPE.ROCK_CANDY_1 ||
            type == ITEM_TYPE.ROCK_CANDY_2 ||
            type == ITEM_TYPE.ROCK_CANDY_3 ||
            type == ITEM_TYPE.ROCK_CANDY_4 ||
            type == ITEM_TYPE.ROCK_CANDY_5 ||
            type == ITEM_TYPE.ROCK_CANDY_6 ||
            type == ITEM_TYPE.BREAKABLE ||
            type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9 ||
            type == ITEM_TYPE.COLLECTIBLE_10 ||
            type == ITEM_TYPE.COLLECTIBLE_11 ||
            type == ITEM_TYPE.COLLECTIBLE_12 ||
            type == ITEM_TYPE.COLLECTIBLE_13 ||
            type == ITEM_TYPE.COLLECTIBLE_14 ||
            type == ITEM_TYPE.COLLECTIBLE_15 ||
            type == ITEM_TYPE.COLLECTIBLE_16 ||
            type == ITEM_TYPE.COLLECTIBLE_17 ||
            type == ITEM_TYPE.COLLECTIBLE_18 ||
            type == ITEM_TYPE.COLLECTIBLE_19 ||
            type == ITEM_TYPE.COLLECTIBLE_20
            )
        {
            return false;
        }

        return true;
    }

    public bool Destroyable()
    {
        if (type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9 ||
            type == ITEM_TYPE.COLLECTIBLE_10 ||
            type == ITEM_TYPE.COLLECTIBLE_11 ||
            type == ITEM_TYPE.COLLECTIBLE_12 ||
            type == ITEM_TYPE.COLLECTIBLE_13 ||
            type == ITEM_TYPE.COLLECTIBLE_14 ||
            type == ITEM_TYPE.COLLECTIBLE_15 ||
            type == ITEM_TYPE.COLLECTIBLE_16 ||
            type == ITEM_TYPE.COLLECTIBLE_17 ||
            type == ITEM_TYPE.COLLECTIBLE_18 ||
            type == ITEM_TYPE.COLLECTIBLE_19 ||
            type == ITEM_TYPE.COLLECTIBLE_20)
        {
            return false;
        }

        return true;
    }

    public bool IsCookie()
    {
        if (type == ITEM_TYPE.BlueBox ||
            type == ITEM_TYPE.GreenBox ||
            type == ITEM_TYPE.ORANGEBOX ||
            type == ITEM_TYPE.PURPLEBOX ||
            type == ITEM_TYPE.REDBOX ||
            type == ITEM_TYPE.YELLOWBOX)
        {
            return true;
        }

        return false;

    }

    public bool IsCollectible()
    {
        if (type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9 ||
            type == ITEM_TYPE.COLLECTIBLE_10 ||
            type == ITEM_TYPE.COLLECTIBLE_11 ||
            type == ITEM_TYPE.COLLECTIBLE_12 ||
            type == ITEM_TYPE.COLLECTIBLE_13 ||
            type == ITEM_TYPE.COLLECTIBLE_14 ||
            type == ITEM_TYPE.COLLECTIBLE_15 ||
            type == ITEM_TYPE.COLLECTIBLE_16 ||
            type == ITEM_TYPE.COLLECTIBLE_17 ||
            type == ITEM_TYPE.COLLECTIBLE_18 ||
            type == ITEM_TYPE.COLLECTIBLE_19 ||
            type == ITEM_TYPE.COLLECTIBLE_20)
        {
            return true;
        }

        return false;
    }

    public bool IsGingerbread()
    {
        if (type == ITEM_TYPE.ROCKET_1 ||
            type == ITEM_TYPE.ROCKET_2 ||
            type == ITEM_TYPE.ROCKET_3 ||
            type == ITEM_TYPE.ROCKET_4 ||
            type == ITEM_TYPE.ROCKET_5 ||
            type == ITEM_TYPE.ROCKET_6)
        {
            return true;
        }

        return false;
    }

    public bool IsMarshmallow()
    {
        if (type == ITEM_TYPE.BREAKABLE)
        {
            return true;
        }
        return false;
    }

    public bool IsChocolate()
    {
        if (type == ITEM_TYPE.MINE_1_LAYER ||
            type == ITEM_TYPE.MINE_2_LAYER ||
            type == ITEM_TYPE.MINE_3_LAYER ||
            type == ITEM_TYPE.MINE_4_LAYER ||
            type == ITEM_TYPE.MINE_5_LAYER ||
            type == ITEM_TYPE.MINE_6_LAYER)
        {
            return true;
        }

        return false;
    }

    public bool IsRockCandy()
    {
        if (type == ITEM_TYPE.ROCK_CANDY_1 ||
            type == ITEM_TYPE.ROCK_CANDY_2 ||
            type == ITEM_TYPE.ROCK_CANDY_3 ||
            type == ITEM_TYPE.ROCK_CANDY_4 ||
            type == ITEM_TYPE.ROCK_CANDY_5 ||
            type == ITEM_TYPE.ROCK_CANDY_6)
        {
            return true;
        }

        return false;
    }

    public ITEM_TYPE OriginCookieType()
    {
        var order = board.NodeOrder(node.i, node.j);

        return StageLoader.instance.itemLayerData[order];
    }

    ITEM_TYPE GetColRowBreaker(ITEM_TYPE check, Vector3 direction)
    {


        if (Random.Range(0, 2) == 0)
        {

            switch (check)
            {
                case ITEM_TYPE.BlueBox:
                case ITEM_TYPE.GreenBox:
                case ITEM_TYPE.ORANGEBOX:
                case ITEM_TYPE.PURPLEBOX:
                case ITEM_TYPE.REDBOX:
                case ITEM_TYPE.YELLOWBOX:
                case ITEM_TYPE.ITEM_ROW:
                case ITEM_TYPE.ITEM_COLUMN:
                    return ITEM_TYPE.ITEM_ROW;
                default:
                    return ITEM_TYPE.NONE;
            }
        }
        else
        {

            switch (check)
            {
                case ITEM_TYPE.BlueBox:
                case ITEM_TYPE.GreenBox:
                case ITEM_TYPE.ORANGEBOX:
                case ITEM_TYPE.PURPLEBOX:
                case ITEM_TYPE.REDBOX:
                case ITEM_TYPE.YELLOWBOX:
                case ITEM_TYPE.ITEM_ROW:
                case ITEM_TYPE.ITEM_COLUMN:
                    return ITEM_TYPE.ITEM_COLUMN;
                default:
                    return ITEM_TYPE.NONE;
            }
        }
    }

    public bool IsBombBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_BOMB)
        {
            return true;
        }

        return false;
    }

    public bool IsMedBombBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_MEDBOMB)
        {
            return true;
        }

        return false;
    }

    public bool IsBigBombBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_BIGBOMB)
        {
            return true;
        }

        return false;
    }


    public bool IsXBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_CROSS)
        {
            return true;
        }

        return false;
    }
    public bool IsColorHunterBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_COLORHUNTER1 ||
            check == ITEM_TYPE.ITEM_COLORHUNTER2 ||
            check == ITEM_TYPE.ITEM_COLORHUNTER3 ||
            check == ITEM_TYPE.ITEM_COLORHUNTER4 ||
            check == ITEM_TYPE.ITEM_COLORHUNTER5 ||
            check == ITEM_TYPE.ITEM_COLORHUNTER6)
        {
            return true;
        }

        return false;
    }

    public bool IsColumnBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_COLUMN)
        {
            return true;
        }

        return false;
    }

    public bool IsRowBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.ITEM_ROW)
        {
            return true;
        }

        return false;
    }

    public bool IsBreaker(ITEM_TYPE check)
    {
        if (IsBombBreaker(check) || IsXBreaker(check) || IsColumnBreaker(check) || IsRowBreaker(check))
        {
            return true;
        }

        return false;
    }
    public bool IsBooster()
    {

        if (type == ITEM_TYPE.ITEM_ROW
            || type == ITEM_TYPE.ITEM_COLUMN
            || type == ITEM_TYPE.ITEM_CROSS
            || type == ITEM_TYPE.ITEM_BOMB
            || type == ITEM_TYPE.ITEM_MEDBOMB
            || type == ITEM_TYPE.ITEM_BIGBOMB
            || IsColorHunterBreaker(type)
            || type == ITEM_TYPE.ITEM_COLORCONE
            )
        {
            return true;
        }

        return false;

    }
    public bool IsBoosterItem(ITEM_TYPE check)
    {

        if (check == ITEM_TYPE.ITEM_ROW
            || check == ITEM_TYPE.ITEM_COLUMN
            || check == ITEM_TYPE.ITEM_CROSS
            || check == ITEM_TYPE.ITEM_BOMB
            || check == ITEM_TYPE.ITEM_MEDBOMB
            || check == ITEM_TYPE.ITEM_BIGBOMB
            || IsColorHunterBreaker(check)
            || check == ITEM_TYPE.ITEM_COLORCONE
            )
        {
            return true;
        }

        return false;

    }
    public bool KomsularBomb()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_BOMB) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_BOMB) ||
            (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_BOMB) ||
            (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_BOMB)
            )

        {
            return true;
        }
        return false;
    }
    public bool KomsularRowOrColumn()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_COLUMN)
             )
        {
            return true;
        }
        return false;
    }

    public bool KomsularRow()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_ROW) ||
             (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_ROW)
             )
        {
            return true;
        }
        return false;
    }

    public bool KomsularColumn()
    {
        if (
             (komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_COLUMN) ||
             (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_COLUMN)
             )
        {
            return true;
        }
        return false;
    }
    public bool KomsularCross()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_CROSS) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_CROSS) ||
            (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_CROSS) ||
            (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_CROSS)
            )

        {
            return true;
        }
        return false;
    }
    public bool KomsularColorCone()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_COLORCONE) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_COLORCONE) ||
            (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_COLORCONE) ||
            (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_COLORCONE)
            )

        {
            return true;
        }
        return false;
    }
    public bool KomsularBigBomb()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_BIGBOMB) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_BIGBOMB) ||
           (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_BIGBOMB) ||
            (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_BIGBOMB)
            )

        {
            return true;
        }
        return false;
    }
    public bool KomsularMedBomb()
    {
        if ((komsuNodeLeft != null && komsuNodeLeft.type == ITEM_TYPE.ITEM_MEDBOMB) ||
             (komsuNodeRight != null && komsuNodeRight.type == ITEM_TYPE.ITEM_MEDBOMB) ||
           (komsuNodeTop != null && komsuNodeTop.type == ITEM_TYPE.ITEM_MEDBOMB) ||
            (komsuNodeBottom != null && komsuNodeBottom.type == ITEM_TYPE.ITEM_MEDBOMB)
            )

        {
            return true;
        }
        return false;
    }
    public bool KomsularHunter()
    {
        if ((komsuNodeLeft != null && IsColorHunterBreaker(komsuNodeLeft.type)) ||
            (komsuNodeRight != null && IsColorHunterBreaker(komsuNodeRight.type)) ||
           (komsuNodeTop != null && IsColorHunterBreaker(komsuNodeTop.type)) ||
           (komsuNodeBottom != null && IsColorHunterBreaker(komsuNodeBottom.type))
            )

        {
            return true;
        }
        return false;
    }

    public bool KomsularAnyBomb()
    {
        if (KomsularBomb() || KomsularBigBomb() || KomsularMedBomb())
        {
            return true;
        }

        return false;

    }


    public ITEM_TYPE GetBombBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_BOMB;

            default:
                return ITEM_TYPE.NONE;
        }
    }
    public ITEM_TYPE GetMedBombBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_MEDBOMB;
            default:
                return ITEM_TYPE.NONE;
        }
    }
    public ITEM_TYPE GetBigBombBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_BIGBOMB;
            default:
                return ITEM_TYPE.NONE;
        }
    }
    public ITEM_TYPE GetColorConeBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_COLORCONE;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetXBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_CROSS;
            default:
                return ITEM_TYPE.NONE;
        }
    }
    public ITEM_TYPE GetHunterBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
                return ITEM_TYPE.ITEM_COLORHUNTER1;
            case ITEM_TYPE.GreenBox:
                return ITEM_TYPE.ITEM_COLORHUNTER2;
            case ITEM_TYPE.ORANGEBOX:
                return ITEM_TYPE.ITEM_COLORHUNTER3;
            case ITEM_TYPE.PURPLEBOX:
                return ITEM_TYPE.ITEM_COLORHUNTER4;
            case ITEM_TYPE.REDBOX:
                return ITEM_TYPE.ITEM_COLORHUNTER5;
            case ITEM_TYPE.YELLOWBOX:
                return ITEM_TYPE.ITEM_COLORHUNTER6;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetColumnBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_COLUMN;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetRowBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
            case ITEM_TYPE.GreenBox:
            case ITEM_TYPE.ORANGEBOX:
            case ITEM_TYPE.PURPLEBOX:
            case ITEM_TYPE.REDBOX:
            case ITEM_TYPE.YELLOWBOX:
            case ITEM_TYPE.ITEM_ROW:
            case ITEM_TYPE.ITEM_COLUMN:
                return ITEM_TYPE.ITEM_ROW;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetCookie(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.BlueBox:
                return ITEM_TYPE.BlueBox;
            case ITEM_TYPE.GreenBox:
                return ITEM_TYPE.GreenBox;
            case ITEM_TYPE.ORANGEBOX:
                return ITEM_TYPE.ORANGEBOX;
            case ITEM_TYPE.PURPLEBOX:
                return ITEM_TYPE.PURPLEBOX;
            case ITEM_TYPE.REDBOX:
                return ITEM_TYPE.REDBOX;
            case ITEM_TYPE.YELLOWBOX:
                return ITEM_TYPE.YELLOWBOX;
            default:
                return ITEM_TYPE.NONE;
        }

    }

    #endregion

    #region Touch
    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    void checkNeighbor()
    {
        if (node != null)
        {

            if (board.droppingItems > 0 || board.flyingItems > 0 || board.destroyingItems > 0 || board.huntering || board.merging)
            {
                //{
                //    if (!changedSprite)
                //    {
                //        changedSprite = true;

                CheckStat();
            }

        }
        //else
        //{
        //    changedSprite = false;
        //}



    }

    void getNeighbor()
    {
        if (neighborNode == null)
        {
            swapItem = this;
            board.touchedItem = swapItem;
            board.swappedItem = swapItem;
        }
        OnStartSwap();
        OnCompleteSwap();
        if (IsCookie())
        { getChangeSprite(); }

        Reset();

    }

    public void getChangeSprite()
    {

        //if(StageLoader.instance.debug)
        //{
        //    ResetChangeSprite(color);
        //    return;
        //}
        var matchesHere = (node.FindMatches() != null) ? node.FindMatches().Count : 0;

        if (matchesHere == 5 || matchesHere == 6)
        {
            ShapeSprite.sprite = Shape[8];
        }
        else if (matchesHere == 7)
        {
            ShapeSprite.sprite = Shape[7];
        }
        else if (matchesHere == 8)
        {
            ShapeSprite.sprite = Shape[9];
        }
        else if (matchesHere == 9)
        {
            ShapeSprite.sprite = Shape[10];
        }
        else if (matchesHere >= 10)
        {
            ShapeSprite.sprite = Shape[12];
        }

        else
        {
            ResetChangeSprite(color);
            //ResetChangeSprite(0);
        }
    }
    public void getChangeSpriteBoosters()
    {

        var matchesBoosterHere = (node.FindMatchesBooster() != null) ? node.FindMatchesBooster().Count : 0;

        if (matchesBoosterHere >= 2 && IsBooster())
        {

            try { MergeBoosterEffect.gameObject.SetActive(true); }
            catch { }

        }
        else

        {
            try { MergeBoosterEffect.gameObject.SetActive(false); }
            catch { }

        }

    }
    public void ResetChangeSprite(int color)
    {

        ShapeSprite.sprite = Shape[color];

    }

    public void OnStartSwap()
    {

        AudioManager.instance.SwapAudio();

        board.lockSwap = false;

        board.dropTime = 0;
    }


    public void OnCompleteSwap()
    {
        var matchesHere = (node.FindMatches() != null) ? node.FindMatches().Count : 0;
        var matchesAtNeighbor = (swapItem.node.FindMatches() != null) ? swapItem.node.FindMatches().Count : 0;
        var special = false;

        var matchesBoosterHere = (node.FindMatchesBooster() != null) ? node.FindMatchesBooster().Count : 0;
        var matchesAtBoosterNeighbor = (swapItem.node.FindMatchesBooster() != null) ? swapItem.node.FindMatchesBooster().Count : 0;


        if (type == ITEM_TYPE.ITEM_COLORCONE && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.ITEM_COLORCONE))
        {
            special = true;
        }
        else if (swapItem.type == ITEM_TYPE.ITEM_COLORCONE && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.ITEM_COLORCONE))
        {
            special = true;
        }
        //
        if (type == ITEM_TYPE.ITEM_BIGBOMB && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.ITEM_BIGBOMB))
        {
            special = true;
        }
        else if (swapItem.type == ITEM_TYPE.ITEM_BIGBOMB && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.ITEM_BIGBOMB))
        {
            special = true;
        }
        if (IsColorHunterBreaker(type) && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.ITEM_BIGBOMB || IsColorHunterBreaker(swapItem.type)))
        {
            special = true;
        }
        else if (IsColorHunterBreaker(swapItem.type) && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.ITEM_BIGBOMB || IsColorHunterBreaker(type)))
        {
            special = true;
        }
        if (type == ITEM_TYPE.ITEM_MEDBOMB && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.ITEM_MEDBOMB))
        {
            special = true;
        }
        else if (swapItem.type == ITEM_TYPE.ITEM_MEDBOMB && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.ITEM_MEDBOMB))
        {
            special = true;
        }
        //
        if (IsBreaker(type) && IsBreaker(swapItem.type))
        {
            special = true;
        }



        if (special == true && matchesBoosterHere < 2)
        {
            SingleBooster(this, swapItem);
            RainbowDestroy(this, swapItem);
            ColorHunterDestroy(this);

        }
        else if (matchesBoosterHere >= 2)
        {

            //MERGE BOOSTER

            Row_Row_Destroy(this, KomsuItems);
            Row_Bomb_Destroy(this, KomsuItems);
            Row_Cross_Destroy(this, KomsuItems);
            Cross_Cross_Destroy(this, KomsuItems);
            Row_Hunter_Destroy(this, KomsuItems);

            Bomb_Bomb_Destroy(this, KomsuItems);
            Cross_Bomb_Destroy(this, KomsuItems);
            Bomb_Med_Destroy(this, KomsuItems);
            Bomb_Big_Destroy(this, KomsuItems);
            Bomb_Hunter_Destroy(this, KomsuItems);

            Cross_Hunter_Destroy(this, KomsuItems);

            Med_Med_Destroy(this, KomsuItems);
            Med_Big_Destroy(this, KomsuItems);

            Big_Big_Destroy(this, KomsuItems);

            Rainbow_Merge_Destroy(this, KomsuItems);
            Hunter_Hunter_Destroy(this, KomsuItems);


        }
        else
        {

            if (matchesHere == 5 || matchesHere == 6)
            {
                CameraSize.instance.MinShake = true;
                next = GetBombBreaker(this.type);

            }
            else if (matchesAtNeighbor == 5 || matchesAtNeighbor == 6)
            {
                CameraSize.instance.MinShake = true;
                swapItem.next = GetBombBreaker(swapItem.type);

            }
            else if (matchesHere == 7)
            {
                CameraSize.instance.MinShake = true;
                next = GetColRowBreaker(this.type, transform.position - swapItem.transform.position);
            }
            else if (matchesAtNeighbor == 7)
            {
                CameraSize.instance.MinShake = true;
                swapItem.next = GetColRowBreaker(swapItem.type, transform.position - swapItem.transform.position);
            }
            else if (matchesHere == 8)
            {
                CameraSize.instance.MinShake = true;
                next = GetXBreaker(this.type);
            }
            else if (matchesAtNeighbor == 8)
            {
                CameraSize.instance.MinShake = true;
                swapItem.next = GetXBreaker(swapItem.type);
            }
            else if (matchesHere == 9)
            {
                CameraSize.instance.MinShake = true;
                next = GetMedBombBreaker(this.type);
            }
            else if (matchesAtNeighbor == 9)
            {
                CameraSize.instance.MinShake = true;
                swapItem.next = GetMedBombBreaker(swapItem.type);
            }
            else if (matchesHere >= 10)
            {
                CameraSize.instance.MinShake = true;
                next = GetHunterBreaker(this.type);
            }
            else if (matchesAtNeighbor >= 10)
            {
                CameraSize.instance.MinShake = true;
                swapItem.next = GetHunterBreaker(swapItem.type);
            }


            board.FindMatches();
        }

        Reset();

    }

    public void OnStartSwapBack()
    {

        AudioManager.instance.SwapBackAudio();
    }

    public void OnCompleteSwapBack()
    {

        transform.position = board.NodeLocalPosition(node.i, node.j);

        Reset();

        StartCoroutine(board.ShowHint());
    }

    public void Reset()
    {
        drag = false;

        neighborNodeBottom = null;
        neighborNodeLeft = null;
        neighborNodeRight = null;
        neighborNodeTop = null;

        neighborNode = null;
        swapItem = null;

    }

    #endregion

    #region ColorAndAppear

    public void GenerateColor(int except)
    {
        var colors = new List<int>();

        var usingColors = StageLoader.instance.usingColors;

        for (int i = 0; i < usingColors.Count; i++)
        {
            int color = usingColors[i];

            bool generatable = true;
            Node neighbor = null;

            neighbor = node.TopNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            neighbor = node.LeftNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            neighbor = node.RightNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            if (generatable && color != except)
            {
                colors.Add(color);
            }
        } // end for

        int index = usingColors[Random.Range(0, usingColors.Count)];
        int count = +1;
        print(index + " :Index: " + count);
        if (colors.Count > 0)
        {
            index = colors[Random.Range(0, colors.Count)];
        }

        if (index == except)
        {
            index = (index++) % usingColors.Count;
        }

        this.color = index;

        ChangeSpriteAndType(index);
    }

    public void ChangeSpriteAndType(int itemColor)
    {
        GameObject prefab = null;

        switch (itemColor)
        {
            case 1:
                prefab = Resources.Load(Configuration.Item1()) as GameObject;
                type = ITEM_TYPE.BlueBox;
                break;
            case 2:
                prefab = Resources.Load(Configuration.Item2()) as GameObject;
                type = ITEM_TYPE.GreenBox;
                break;
            case 3:
                prefab = Resources.Load(Configuration.Item3()) as GameObject;
                type = ITEM_TYPE.ORANGEBOX;
                break;
            case 4:
                prefab = Resources.Load(Configuration.Item4()) as GameObject;
                type = ITEM_TYPE.PURPLEBOX;
                break;
            case 5:
                prefab = Resources.Load(Configuration.Item5()) as GameObject;
                type = ITEM_TYPE.REDBOX;
                break;
            case 6:
                prefab = Resources.Load(Configuration.Item6()) as GameObject;
                type = ITEM_TYPE.YELLOWBOX;
                break;
        }

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
        CheckStat();
    }

    public void ChangeToRainbow()
    {
        StartCoroutine(StartChangeToRainbow());
    }

    IEnumerator StartChangeToRainbow()
    {

        CameraSize.instance.MinShake = true;
        var prefab = Resources.Load(Configuration.ItemColorCone()) as GameObject;
        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }

        type = ITEM_TYPE.ITEM_COLORCONE;


        //color = 15;
        color = Random.Range(333, 343);
        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        }


        CheckStat();
        yield return new WaitForSeconds(0.5f);

    }

    public void ChangeToBigBomb()
    {
        CameraSize.instance.MinShake = true;
        var prefab = Resources.Load(Configuration.BigBomb()) as GameObject;

        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }

        //color = 13;
        color = Random.Range(333, 343);
        GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        type = ITEM_TYPE.ITEM_BIGBOMB;

        CheckStat();
    }
    public void ChangeToMedBomb()
    {
        CameraSize.instance.MinShake = true;
        var prefab = Resources.Load(Configuration.MedBomb()) as GameObject;

        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }

        //color = 11;
        color = Random.Range(333, 343);
        GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        type = ITEM_TYPE.ITEM_MEDBOMB;

        CheckStat();
    }

    public void ChangeToGingerbread(ITEM_TYPE check)
    {
        if (node.item.IsGingerbread() == true)
        {
            return;
        }

        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }
        var upper = board.GetUpperItem(this.node);

        if (upper != null && upper.IsGingerbread())
        {
            return;
        }

        AudioManager.instance.GingerbreadAudio();

        GameObject explosion = null;

        switch (node.item.type)
        {
            case ITEM_TYPE.BlueBox:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GreenBox:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ORANGEBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.PURPLEBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.REDBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.YELLOWBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
                break;
        }

        if (explosion != null) explosion.transform.position = node.item.transform.position;

        GameObject prefab = null;

        switch (check)
        {
            case ITEM_TYPE.ROCKET_1:
                prefab = Resources.Load(Configuration.Rocket1()) as GameObject;
                check = ITEM_TYPE.ROCKET_1;
                color = 1;
                break;
            case ITEM_TYPE.ROCKET_2:
                prefab = Resources.Load(Configuration.Rocket2()) as GameObject;
                check = ITEM_TYPE.ROCKET_2;
                color = 2;
                break;
            case ITEM_TYPE.ROCKET_3:
                prefab = Resources.Load(Configuration.Rocket3()) as GameObject;
                check = ITEM_TYPE.ROCKET_3;
                color = 3;
                break;
            case ITEM_TYPE.ROCKET_4:
                prefab = Resources.Load(Configuration.Rocket4()) as GameObject;
                check = ITEM_TYPE.ROCKET_4;
                color = 4;
                break;
            case ITEM_TYPE.ROCKET_5:
                prefab = Resources.Load(Configuration.Rocket5()) as GameObject;
                check = ITEM_TYPE.ROCKET_5;
                color = 5;
                break;
            case ITEM_TYPE.ROCKET_6:
                prefab = Resources.Load(Configuration.Rocket6()) as GameObject;
                check = ITEM_TYPE.ROCKET_6;
                color = 6;
                break;
        }


        if (prefab != null)
        {
            type = check;
            effect = BREAKER_EFFECT.NORMAL;

            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
        CheckStat();
    }

    public void ChangeToBombBreaker()
    {

        StartCoroutine(StartChangeToBombBreaker());
    }

    IEnumerator StartChangeToBombBreaker()
    {


        CameraSize.instance.MinShake = true;
        GameObject prefab = null;
        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }

        prefab = Resources.Load(Configuration.Item1Bomb()) as GameObject;
        type = ITEM_TYPE.ITEM_BOMB;


        color = Random.Range(333, 343);

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

        }
        CheckStat();

        yield return new WaitForSeconds(0.1f);
        iTween.ScaleTo(this.gameObject, iTween.Hash(
                      "scale", new Vector3(1f, 1f, 0),
                      // "oncomplete", "CompleteChangeToBig",
                      "easetype", iTween.EaseType.easeOutBounce,
                      "time", Configuration.instance.changingTime
                  ));


    }


    public void ChangeToXBreaker()
    {
        CameraSize.instance.MinShake = true;
        GameObject prefab = null;
        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }
        color = Random.Range(333, 343);
        prefab = Resources.Load(Configuration.Item1Cross()) as GameObject;
        type = ITEM_TYPE.ITEM_CROSS;



        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
        CheckStat();
    }
    public void ChangeToHunterBreaker()
    {
        CameraSize.instance.MinShake = true;
        if (IsCookie())
        {
            ShapeSprite.gameObject.SetActive(false);
        }
        GameObject prefab = null;

        int newcolor = this.color;
        destroycolor = newcolor;
        if (newcolor == 1)
        {
            prefab = Resources.Load(Configuration.ColorHunter1()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER1;
        }
        else if (newcolor == 2)
        {
            prefab = Resources.Load(Configuration.ColorHunter2()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER2;
        }
        else if (newcolor == 3)
        {
            prefab = Resources.Load(Configuration.ColorHunter3()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER3;
        }
        else if (newcolor == 4)
        {
            prefab = Resources.Load(Configuration.ColorHunter4()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER4;
        }
        else if (newcolor == 5)
        {
            prefab = Resources.Load(Configuration.ColorHunter5()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER5;
        }
        else if (newcolor == 6)
        {
            prefab = Resources.Load(Configuration.ColorHunter6()) as GameObject;
            type = ITEM_TYPE.ITEM_COLORHUNTER6;
        }


        color = Random.Range(333, 343);

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
        CheckStat();
    }
    public void ChangeToColRowBreaker()
    {
        CameraSize.instance.MinShake = true;
        GameObject prefab = null;

        if (Random.Range(0, 2) == 0)
        {

            prefab = Resources.Load(Configuration.Item1Column()) as GameObject;
            if (IsCookie())
            {
                ShapeSprite.gameObject.SetActive(false);
            }
            if (prefab != null)
            {
                GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            }

            type = ITEM_TYPE.ITEM_COLUMN;

        }
        else
        {

            prefab = Resources.Load(Configuration.Item1Row()) as GameObject;
            if (IsCookie())
            {
                ShapeSprite.gameObject.SetActive(false);
            }
            if (prefab != null)
            {
                GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
            }

            type = ITEM_TYPE.ITEM_ROW;
        }

        color = Random.Range(333, 343);


        CheckStat();

    }

    public void SetRandomNextType()
    {
        var random = Random.Range(0, 3);

        if (random == 0)
        {
            next = ITEM_TYPE.ITEM_COLUMN;
        }
        else if (random == 1)
        {
            next = ITEM_TYPE.ITEM_ROW;

        }
        else if (random == 2)
        {
            next = ITEM_TYPE.ITEM_BOMB;
        }

    }

    #endregion

    #region Destroy

    public void Destroy(bool forced = false)
    {
        if (Destroyable() == false && forced == false)
        {
            return;
        }

        if (destroying == true) return;
        else destroying = true;

        if (node != null && node.cage != null)
        {
            node.CageExplode();
            return;
        }

        board.destroyingItems++;


        if (IsBooster() && !board.GameOver && !board.merging && !board.huntering && !this.IsColorHunterBreaker(this.type))// && matchesBoosterHere < 2)
        {
            StartCoroutine(DestroyBooster());
        }
        else if (this.IsColorHunterBreaker(this.type) && !board.merging && !board.huntering)// && matchesBoosterHere < 2)
        {
            ColorHunterDestroy(this);
        }
        else
        {

            iTween.ScaleTo(gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.linear,
         "time", 0
     ));
            
        }

        board.Destroyed = true;
    }


    IEnumerator DestroyBooster()
    {
        while (board.droppingItems > 0 || board.boosterdestroying)
        {
            yield return new WaitForEndOfFrame();
        }

        board.boosterdestroying = true;

        //yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sortingOrder = 102;

        GetComponent<SpriteRenderer>().sortingLayerName = "Effect";

        //yield return new WaitForSeconds(0.1f);
        AudioManager.instance.BombBreakerAudio();
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(2.8f, 2.8f, 0),
            "easetype", iTween.EaseType.easeOutBounce,
            "time", 0.3f
        ));
        yield return new WaitForSeconds(0.4f);
        //board.BoosterArrount(node.item);
        //yield return new WaitForSeconds(0.2f);


        iTween.ScaleTo(gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInOutBack,
       "time", Configuration.instance.destroyTime
   ));
        board.boosterdestroying = false;


    }


    public void OnStartDestroy()
    {

        if (node != null) node.WaffleExplode();

        board.CollectItem(this);
        board.DestroyNeighborItems(this);
        Item boosterItem = this;

        if (effect == BREAKER_EFFECT.BIG_COLUMN_BREAKER)
        {
            BigColumnBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.BIG_ROW_BREAKER)
        {
            BigRowBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.CROSS)
        {
            CrossBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.BOMB_X_BREAKER)
        {
            BombXBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.CROSS_X_BREAKER)
        {
            CrossXBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.COLUMN_EFFECT)
        {
            col_BreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.ROW_EFFECT)
        {
            row_BreakerExplosion();

        }
        else if (effect == BREAKER_EFFECT.NORMAL)
        {
            if (IsCookie())
            {
                CookieExplosion();
            }
            else if (IsGingerbread())
            {
                GingerbreadExplosion();
            }
            else if (IsMarshmallow())
            {
                MarshmallowExplosion();
            }
            else if (IsChocolate())
            {
                ChocolateExplosion();
            }
            else if (IsRockCandy())
            {
                RockCandyExplosion();
            }
            else if (IsCollectible())
            {
                CollectibleExplosion();
            }
            else if (IsBombBreaker(type))
            {
                BombBreakerExplosion();
            }
            else if (IsXBreaker(type))
            {
                XBreakerExplosion();
            }
            else if (type == ITEM_TYPE.ITEM_COLORCONE)
            {
                RainbowExplosion();
            }
            else if (type == ITEM_TYPE.ITEM_BIGBOMB)
            {
                BigBombExplosion();
            }
            else if (type == ITEM_TYPE.ITEM_MEDBOMB)
            {
                MedBombExplosion();
            }
            else if (IsColumnBreaker(type))
            {
                ColumnBreakerExplosion();
            }
            else if (IsRowBreaker(type))
            {
                RowBreakerExplosion();
            }


        }
    }

    public void OnCompleteDestroy()
    {
        if (board.state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            board.score += Configuration.instance.finishedScoreItem * board.dropTime;
        }
        else
        {
            board.score += Configuration.instance.scoreItem / 2 + Configuration.instance.scoreItem * board.dropTime;
        }

        board.UITop.UpdateProgressBar(board.score);

        board.UITop.UpdateScoreAmount(board.score);

        if (next != ITEM_TYPE.NONE)
        {

            if (IsBombBreaker(next) || IsXBreaker(next))
            {
                if (nextSound == true) AudioManager.instance.BombBreakerAudio();
            }
            else if (IsRowBreaker(next) || IsColumnBreaker(next))
            {
                if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            }
            else if (next == ITEM_TYPE.ITEM_COLORCONE)
            {
                if (nextSound == true) AudioManager.instance.RainbowAudio();
            }
            else if (next == ITEM_TYPE.ITEM_BIGBOMB)
            {
                if (nextSound == true) AudioManager.instance.BombBreakerAudio();
            }
            else if (next == ITEM_TYPE.ITEM_MEDBOMB)
            {
                if (nextSound == true) AudioManager.instance.BombBreakerAudio();
            }
            else if (IsColorHunterBreaker(next))
            {
                if (nextSound == true) AudioManager.instance.BombBreakerAudio();
            }

            //StartCoroutine(StartGenerateItem());
            node.GenerateItem(next);
        }
        else if (type == ITEM_TYPE.MINE_2_LAYER)
        {
            node.GenerateItem(ITEM_TYPE.MINE_1_LAYER);

            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else if (type == ITEM_TYPE.MINE_3_LAYER)
        {
            node.GenerateItem(ITEM_TYPE.MINE_2_LAYER);

            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else if (type == ITEM_TYPE.MINE_4_LAYER)
        {
            node.GenerateItem(ITEM_TYPE.MINE_3_LAYER);

            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else
        {
            node.item = null;

        }

        if (destroying == true)
        {
            board.destroyingItems--;

            if (dropping == true) board.droppingItems--;

            GameObject.Destroy(gameObject);
        }
        if (board.merging)
        {
            board.merging = false;
        }
    }


    public IEnumerator ResetDestroying()
    {
        yield return new WaitForSeconds(Configuration.instance.destroyTime);

        destroying = false;
    }

    #endregion

    #region Explosion


    void CookieExplosion()
    {
        AudioManager.instance.CookieCrushAudio();

        GameObject explosion = null;


        switch (type)
        {
            case ITEM_TYPE.BlueBox:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("1", PlayerPrefs.GetInt("1") + 1);
                    PlayerPrefs.SetInt("BlueBox", PlayerPrefs.GetInt("BlueBox") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_1", 1);
                    
                }
                break;
            case ITEM_TYPE.GreenBox:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("2", PlayerPrefs.GetInt("2") + 1);
                    PlayerPrefs.SetInt("GreenBox", PlayerPrefs.GetInt("GreenBox") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_2", 1);
                }
                break;
            case ITEM_TYPE.ORANGEBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("3", PlayerPrefs.GetInt("3") + 1);
                    PlayerPrefs.SetInt("OrangeBox", PlayerPrefs.GetInt("OrangeBox") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_3", 1);
                }
                break;
            case ITEM_TYPE.PURPLEBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("4", PlayerPrefs.GetInt("4") + 1);
                    PlayerPrefs.SetInt("Sari", PlayerPrefs.GetInt("Sari") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_4", 1);
                }
                break;
            case ITEM_TYPE.REDBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("5", PlayerPrefs.GetInt("5") + 1);
                    PlayerPrefs.SetInt("RedBox", PlayerPrefs.GetInt("RedBox") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_5", 1);
                }
                break;
            case ITEM_TYPE.YELLOWBOX:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
                if (!board.GameOver)
                {
                    PlayerPrefs.SetInt("6", PlayerPrefs.GetInt("6") + 1);
                    PlayerPrefs.SetInt("YellowBox", PlayerPrefs.GetInt("YellowBox") + 1);
                    PlayerPrefs.Save();
                    //Achievement
                    Configuration.SaveAchievement("ach_collectCubes_6", 1);
                }
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    void GingerbreadExplosion()
    {
        AudioManager.instance.GingerbreadExplodeAudio();

        GameObject explosion = null;

        switch (type)
        {
            case ITEM_TYPE.ROCKET_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCKET_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCKET_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCKET_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCKET_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCKET_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    void MarshmallowExplosion()
    {
        AudioManager.instance.MarshmallowExplodeAudio();

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakableExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    public void ChocolateExplosion()
    {
        AudioManager.instance.ChocolateExplodeAudio();

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.MineExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    public void RockCandyExplosion()
    {
        AudioManager.instance.RockCandyExplodeAudio();

        GameObject explosion = null;

        switch (type)
        {
            case ITEM_TYPE.ROCK_CANDY_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BlueBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.GreenBoxExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.OrangeCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.PurpleCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RedCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.YellowCookieExplosion()) as GameObject);
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    void CollectibleExplosion()
    {
        AudioManager.instance.CollectibleExplodeAudio();
    }
    void BombBreakerExplosion()
    {
        AudioManager.instance.BombExplodeAudio();

        BombBreakerDestroy();

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion1()) as GameObject);


        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void RainbowExplosion()
    {
        AudioManager.instance.RainbowExplodeAudio();

        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }
    void BigBombExplosion()
    {
        AudioManager.instance.BigBombBreakerAudio();

        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        BigBombBreakerDestroy();

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void GiantBombExplosion()
    {
        AudioManager.instance.BigBombBreakerAudio();

        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        GiantBombBreakerDestroy();

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void MegaGiantBombExplosion()
    {
        AudioManager.instance.BigBombBreakerAudio();

        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        MegaGiantBombBreakerDestroy();

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void MedBombExplosion()
    {
        AudioManager.instance.MedBombBreakerAudio();
        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);



        MedBombBreakerDestroy();

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void ColorHunterExplosion(Item boosterItem)
    {
        AudioManager.instance.MedBombBreakerAudio();
        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

        board.DestroySameColorList(boosterItem);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }
    void XBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        XBreakerDestroy();

        GameObject explosion = null;
        GameObject animation = null;
        GameObject cross = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        animation = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
        //color = 9;
        color = Random.Range(333, 343);

        if (animation != null)
        {
            cross = Instantiate(animation);
            animation.transform.Rotate(Vector3.back, 45);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (cross != null)
        {
            cross.transform.Rotate(Vector3.back, -45);
            cross.transform.position = new Vector3(cross.transform.position.x, cross.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
    }
    void BigColumnBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();


        //ColumnDestroy(node.j - 2);
        ColumnDestroy(node.j - 1);
        ColumnDestroy(node.j);
        ColumnDestroy(node.j + 1);
        //ColumnDestroy(node.j + 2);

        GameObject explosion = null;
        GameObject animation = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        animation = Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;


        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        if (animation != null)
        {
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        GameObject.Destroy(animation, 1f);
    }
    void CrossBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy();
        RowDestroy();

        GameObject explosion = null;
        GameObject columnEffect = null;
        GameObject rowEffect = null;


        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        columnEffect = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;

        if (columnEffect != null)
        {
            rowEffect = Instantiate(columnEffect as GameObject, transform.position, Quaternion.identity) as GameObject;
            columnEffect.transform.position = new Vector3(columnEffect.transform.position.x, columnEffect.transform.position.y, -12f);
        }

        if (rowEffect != null)
        {
            rowEffect.transform.Rotate(Vector3.back, 90);
            rowEffect.transform.position = new Vector3(rowEffect.transform.position.x, rowEffect.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(rowEffect, 1f);

        GameObject.Destroy(columnEffect, 1f);
    }
    void BigCrossBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy(node.j - 1);
        ColumnDestroy(node.j);
        ColumnDestroy(node.j + 1);
        RowDestroy(node.i - 1);
        RowDestroy(node.i);
        RowDestroy(node.i + 1);

        GameObject explosion = null;
        GameObject columnEffect = null;
        GameObject rowEffect = null;
        GameObject animation = null;


        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        columnEffect = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
        animation = Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;

        if (columnEffect != null)
        {
            rowEffect = Instantiate(columnEffect as GameObject, transform.position, Quaternion.identity) as GameObject;
            columnEffect.transform.position = new Vector3(columnEffect.transform.position.x, columnEffect.transform.position.y, -12f);
        }

        if (rowEffect != null)
        {
            rowEffect.transform.Rotate(Vector3.back, 45);
            rowEffect.transform.position = new Vector3(rowEffect.transform.position.x, rowEffect.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(rowEffect, 1f);

        GameObject.Destroy(columnEffect, 1f);
    }
    void GiantCrossBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy(node.j - 1);
        ColumnDestroy(node.j);
        ColumnDestroy(node.j + 1);
        RowDestroy(node.i - 1);
        RowDestroy(node.i);
        RowDestroy(node.i + 1);

        GameObject explosion = null;
        GameObject columnEffect = null;
        GameObject rowEffect = null;
        GameObject animation = null;


        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        columnEffect = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
        animation = Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;

        if (columnEffect != null)
        {
            rowEffect = Instantiate(columnEffect as GameObject, transform.position, Quaternion.identity) as GameObject;
            columnEffect.transform.position = new Vector3(columnEffect.transform.position.x, columnEffect.transform.position.y, -12f);
        }

        if (rowEffect != null)
        {
            rowEffect.transform.Rotate(Vector3.back, 45);
            rowEffect.transform.position = new Vector3(rowEffect.transform.position.x, rowEffect.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(rowEffect, 1f);

        GameObject.Destroy(columnEffect, 1f);
    }
    void RowBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();
        GameObject explosion = null;
        GameObject animation = null;


        RowDestroy();
        animation = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);



        if (animation != null)
        {
            animation.transform.Rotate(Vector3.back, 90);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("8", PlayerPrefs.GetInt("8") + 1);
            PlayerPrefs.Save();
            //achievement
            Configuration.SaveAchievement("ach_blast_rocket", 1);
            //Debug.Log("8: " + PlayerPrefs.GetInt("8"));
        }
    }
    void ColumnBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();
        GameObject explosion = null;
        GameObject animation = null;

        ColumnDestroy();
        animation = Instantiate(Resources.Load(Configuration.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;



        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);



        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        if (animation != null)
        {
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        GameObject.Destroy(animation, 1f);
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("8", PlayerPrefs.GetInt("8") + 1);
            PlayerPrefs.Save();

            //achievement
            Configuration.SaveAchievement("ach_blast_rocket", 1);
        }
    }
    void BigRowBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        //RowDestroy(node.i - 2);
        RowDestroy(node.i - 1);
        RowDestroy(node.i);
        RowDestroy(node.i + 1);
        //RowDestroy(node.i + 2);

        GameObject explosion = null;
        GameObject animation = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.ColRowBreaker1()) as GameObject);
        animation = Instantiate(Resources.Load(Configuration.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;



        if (animation != null)
        {
            animation.transform.Rotate(Vector3.back, 90);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
    }
    void BombXBreakerExplosion()
    {
        BombBreakerExplosion();

        XBreakerExplosion();
    }
    void CrossXBreakerExplosion()
    {
        CrossBreakerExplosion();

        XBreakerExplosion();
    }
    void col_BreakerExplosion()
    {

        ColumnBreakerExplosion();

    }
    void row_BreakerExplosion()
    {
        RowBreakerExplosion();

    }

    #endregion

    #region SpecialDestroy

    public void SingleBooster(Item thisItem, Item otherItem)
    {
        if (thisItem.Destroyable() == false || otherItem.Destroyable() == false)
        {
            return;
        }
        thisItem.Destroy();
        otherItem.Destroy();
        board.FindMatches();
    }
    void BombBreakerDestroy()
    {
        List<Item> items = board.ItemAround(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }
        board.ShowInspiringPopup(0);
    }
    void MedBombBreakerDestroy()
    {
        List<Item> items = board.ItemAroundMed(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }
        board.ShowInspiringPopup(0);
    }
    void BigBombBreakerDestroy()
    {
        List<Item> items = board.ItemAroundBig(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }
        board.ShowInspiringPopup(1);
    }
    void GiantBombBreakerDestroy()
    {
        List<Item> items = board.ItemAroundGiant(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }
        board.ShowInspiringPopup(1);
    }
    void MegaGiantBombBreakerDestroy()
    {
        List<Item> items = board.ItemAroundMegaGiant(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }
        board.ShowInspiringPopup(2);
    }
    void XBreakerDestroy()
    {
        List<Item> items = board.XCrossItems(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                item.Destroy();
            }
        }

        board.ShowInspiringPopup(1);

    }
    // destroy all items wwith the same color (when this color swap with a rainbow)
    public void DestroyHunterItemsSameColor(int destroycolor)
    {
        List<Item> items = board.GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == destroycolor)
                {
                    board.sameColorList.Add(item);
                }
            }
        }

        board.DestroyHunterSameColorList();

        board.ShowInspiringPopup(3);
    }
    IEnumerator ColorHunterStartDestroy(Item boosterItem)
    {

        AudioManager.instance.RainbowBoosterAudio();

        boosterItem.DestroyHunterItemsSameColor(boosterItem.destroycolor);

        yield return new WaitForFixedUpdate();
    }
    // destroy all items wwith the same color (when this color swap with a rainbow)
    public void DestroyItemsSameColorRow(Item boosterItem)
    {
        List<Item> items = board.GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == boosterItem.destroycolor)
                {
                    board.sameColorList.Add(item);
                }
            }
        }

        board.DestroySameColorListRow(boosterItem);

    }
    public void DestroyItemsSameColorBomb(Item boosterItem)
    {
        List<Item> items = board.GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == boosterItem.destroycolor)
                {
                    board.sameColorList.Add(item);
                }
            }
        }

        board.DestroySameColorListBomb(boosterItem);
        board.ShowInspiringPopup(2);

    }
    public void DestroyItemsSameColorCross(Item boosterItem)
    {
        List<Item> items = board.GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == boosterItem.destroycolor)
                {
                    board.sameColorList.Add(item);
                }
            }
        }

        board.DestroySameColorListCross(boosterItem);
    }
    public void RainbowDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem.Destroyable() == false || otherItem.Destroyable() == false)
        {
            return;
        }

        if (thisItem.type == ITEM_TYPE.ITEM_COLORCONE)
        {

            if (otherItem.IsCookie())
            {
                //DestroyItemsSameColor (otherItem.color);

                thisItem.Destroy();
            }

            else if (otherItem.IsBombBreaker(otherItem.type) || otherItem.IsRowBreaker(otherItem.type) || otherItem.IsColumnBreaker(otherItem.type) || otherItem.IsXBreaker(otherItem.type))
            {
                ChangeItemsType(otherItem.color, otherItem.type);

                thisItem.Destroy();
            }

            else if (otherItem.type == ITEM_TYPE.ITEM_COLORCONE)
            {
                board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
            }
        }
        else if (otherItem.type == ITEM_TYPE.ITEM_COLORCONE)
        {

            if (thisItem.IsCookie())
            {
                //DestroyItemsSameColor (thisItem.color);

                otherItem.Destroy();
            }
            else if (thisItem.IsBombBreaker(thisItem.type) || thisItem.IsRowBreaker(thisItem.type) || thisItem.IsColumnBreaker(thisItem.type) || thisItem.IsXBreaker(thisItem.type))
            {
                ChangeItemsType(thisItem.color, thisItem.type);

                otherItem.Destroy();
            }


            else if (thisItem.type == ITEM_TYPE.ITEM_COLORCONE)
            {
                board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
            }
        }
    }
    public void BigBombDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem.Destroyable() == false || otherItem.Destroyable() == false)
        {
            return;
        }

        if (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB)
        {

            if (otherItem.IsCookie())
            {
                //DestroyItemsSameColor (otherItem.color);

                thisItem.Destroy();
            }

            else if (otherItem.IsBombBreaker(otherItem.type) || otherItem.IsRowBreaker(otherItem.type) || otherItem.IsColumnBreaker(otherItem.type) || otherItem.IsXBreaker(otherItem.type))
            {
                ChangeItemsType(otherItem.color, otherItem.type);

                thisItem.Destroy();
            }

            else if (otherItem.type == ITEM_TYPE.ITEM_BIGBOMB)
            {
                //  board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
                board.FindMatches();
            }
        }
        else if (otherItem.type == ITEM_TYPE.ITEM_BIGBOMB)
        {

            if (thisItem.IsCookie())
            {
                //DestroyItemsSameColor (thisItem.color);

                otherItem.Destroy();
            }
            else if (thisItem.IsBombBreaker(thisItem.type) || thisItem.IsRowBreaker(thisItem.type) || thisItem.IsColumnBreaker(thisItem.type) || thisItem.IsXBreaker(thisItem.type))
            {
                ChangeItemsType(thisItem.color, thisItem.type);

                otherItem.Destroy();
            }


            else if (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB)
            {
                //  board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
                board.FindMatches();
            }
        }
    }
    public void MedBombDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem.Destroyable() == false || otherItem.Destroyable() == false)
        {
            return;
        }

        if (thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
        {

            if (otherItem.IsCookie())
            {
                //DestroyItemsSameColor (otherItem.color);

                thisItem.Destroy();
            }

            else if (otherItem.IsBombBreaker(otherItem.type) || otherItem.IsRowBreaker(otherItem.type) || otherItem.IsColumnBreaker(otherItem.type) || otherItem.IsXBreaker(otherItem.type))
            {
                ChangeItemsType(otherItem.color, otherItem.type);

                thisItem.Destroy();
            }

            else if (otherItem.type == ITEM_TYPE.ITEM_MEDBOMB)
            {
                // board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
                board.FindMatches();
            }
        }
        else if (otherItem.type == ITEM_TYPE.ITEM_MEDBOMB)
        {

            if (thisItem.IsCookie())
            {
                //DestroyItemsSameColor (thisItem.color);

                otherItem.Destroy();
            }
            else if (thisItem.IsBombBreaker(thisItem.type) || thisItem.IsRowBreaker(thisItem.type) || thisItem.IsColumnBreaker(thisItem.type) || thisItem.IsXBreaker(thisItem.type))
            {
                ChangeItemsType(thisItem.color, thisItem.type);

                otherItem.Destroy();
            }


            else if (thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
            {
                //  board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
                board.FindMatches();
            }
        }
    }  
    void ColumnDestroy(int col = -1)
    {
        var items = new List<Item>();

        if (col == -1)
        {
            items = board.ColumnItems(node.j);
        }
        else
        {
            items = board.ColumnItems(col);
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.ITEM_COLORCONE)
                {
                    //DestroyItemsSameColor(StageLoader.instance.RandomColor());
                }

                item.Destroy();
            }
        }
    }
    public void RowDestroy(int row = -1)
    {
        var items = new List<Item>();

        if (row == -1)
        {
            items = board.RowItems(node.i);
        }
        else
        {
            items = board.RowItems(row);
        }

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.ITEM_COLORCONE)
                {
                    //DestroyItemsSameColor(StageLoader.instance.RandomColor());                   
                }

                item.Destroy();
            }
        }
    }
    void TwoColRowBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && (IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)))
        {
            thisItem.effect = BREAKER_EFFECT.CROSS;
            otherItem.effect = BREAKER_EFFECT.NONE;

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }
    void simpleColumnBreaderDestroy(Item thisItem, Item otherItem)
    {

        if (thisItem == null || otherItem == null)
        {

            return;
        }

        if ((IsColumnBreaker(thisItem.type)))
        {
            thisItem.effect = BREAKER_EFFECT.COLUMN_EFFECT;

            board.FindMatches();
        }
    }
    void simpleRowBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsRowBreaker(thisItem.type)))
        {

            thisItem.effect = BREAKER_EFFECT.ROW_EFFECT;

            board.FindMatches();
        }
    }
    void TwoBombBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if (IsBombBreaker(thisItem.type) && IsBombBreaker(otherItem.type))
        {
            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }
    void TwoXBreakerDestory(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if (IsXBreaker(thisItem.type) && IsXBreaker(otherItem.type))
        {
            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }
    void ColRowBreakerAndBombBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && IsBombBreaker(otherItem.type))
        {
            if (IsRowBreaker(thisItem.type))
            {
                thisItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
            }
            else if (IsColumnBreaker(thisItem.type))
            {
                thisItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
            }
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.ChangeToBig();
        }
        else if ((IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)) && IsBombBreaker(thisItem.type))
        {
            if (IsRowBreaker(otherItem.type))
            {
                otherItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
            }
            else if (IsColumnBreaker(otherItem.type))
            {
                otherItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
            }
            thisItem.type = otherItem.GetCookie(otherItem.type);

            otherItem.ChangeToBig();
        }
    }
    void ColRowBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsXBreaker(thisItem.type) && (IsColumnBreaker(otherItem.type) || IsRowBreaker(otherItem.type))) ||
            (IsXBreaker(otherItem.type) && (IsColumnBreaker(thisItem.type) || IsRowBreaker(thisItem.type))))
        {
            thisItem.effect = BREAKER_EFFECT.CROSS_X_BREAKER;
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }
    void BombBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsBombBreaker(thisItem.type) && IsXBreaker(otherItem.type)) || (IsBombBreaker(otherItem.type) && IsXBreaker(thisItem.type)))
        {
            thisItem.effect = BREAKER_EFFECT.BOMB_X_BREAKER;
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    //New MERGE BOOSTER SPECIALS
    void Row_Row_Destroy(Item thisItem, Item KomsuItems)
    {


        if ((thisItem.type == ITEM_TYPE.ITEM_ROW && KomsularRowOrColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) && !KomsularCross() ||
            (thisItem.type == ITEM_TYPE.ITEM_COLUMN && KomsularRowOrColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter() && !KomsularCross())

            )
        {
            StartCoroutine(Row_Row_Destroy_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Row_Row_Destroy_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();


        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        RowBreakerExplosion();
        ColumnBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInBack,
       "time", Configuration.instance.destroyTime
       ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        board.ShowInspiringPopup(3);

    }

    void Row_Bomb_Destroy(Item thisItem, Item KomsuItems)
    {


        if ((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
            &&
            (KomsularRow() && KomsularColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (thisItem.type == ITEM_TYPE.ITEM_ROW && KomsularColumn() && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()))
        {
            StartCoroutine(Row_Column_Bomb_Start(thisItem, KomsuItems));
        }
        else if ((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
           &&
           (KomsularRow() && KomsularColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
           (thisItem.type == ITEM_TYPE.ITEM_COLUMN && KomsularRow() && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()))
        {
            StartCoroutine(Row_Column_Bomb_Start(thisItem, KomsuItems));
        }
        else if ((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
            &&
            (KomsularRow() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (thisItem.type == ITEM_TYPE.ITEM_ROW && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()))
        {
            StartCoroutine(Row_Bomb_Start(thisItem, KomsuItems));
        }
        else if ((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)
           &&
           (KomsularColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
           (thisItem.type == ITEM_TYPE.ITEM_COLUMN && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()))
        {
            StartCoroutine(Column_Bomb_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Row_Bomb_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Effect";
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        BigRowBreakerExplosion();
        //BigColumnBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
        "scale", Vector3.zero,
        "onstart", "OnStartDestroy",
        "oncomplete", "OnCompleteDestroy",
        "easetype", iTween.EaseType.easeInBack,
        "time", Configuration.instance.destroyTime
        ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;

        board.FindMatches();
        board.ShowInspiringPopup(3);

    }
    IEnumerator Column_Bomb_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        //BigRowBreakerExplosion();
        BigColumnBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInBack,
       "time", Configuration.instance.destroyTime
       ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();

        board.ShowInspiringPopup(2);


    }
    IEnumerator Row_Column_Bomb_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        BigRowBreakerExplosion();
        BigColumnBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInBack,
       "time", Configuration.instance.destroyTime
       ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        board.ShowInspiringPopup(0);

    }

    void Row_Cross_Destroy(Item thisItem, Item KomsuItems)
    {
        if (((thisItem.type == ITEM_TYPE.ITEM_ROW || thisItem.type == ITEM_TYPE.ITEM_COLUMN)
            && KomsularCross() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            ((thisItem.type == ITEM_TYPE.ITEM_CROSS && (KomsularRowOrColumn() || KomsularCross()) && !KomsularCross() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter())))
        {
            StartCoroutine(Row_Cross_Start(thisItem, KomsuItems));
        }

    }
    IEnumerator Row_Cross_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        BigCrossBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInBack,
       "time", Configuration.instance.destroyTime
       ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("16", PlayerPrefs.GetInt("16") + 1);
            //PlayerPrefs.SetInt("13", PlayerPrefs.GetInt("13") + 1);
            PlayerPrefs.Save();
            Configuration.SaveAchievement("ach_rowcross", 1);

            //Debug.Log("16: " + PlayerPrefs.GetInt("16"));
        }
        board.ShowInspiringPopup(1);

    }

    void Cross_Cross_Destroy(Item thisItem, Item KomsuItems)
    {
        if ((thisItem.type == ITEM_TYPE.ITEM_CROSS
            && KomsularCross() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            ((thisItem.type == ITEM_TYPE.ITEM_CROSS && (KomsularCross()) && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter())))
        {
            StartCoroutine(Row_Cross_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Cross_Cross_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        BigCrossBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
       "scale", Vector3.zero,
       "onstart", "OnStartDestroy",
       "oncomplete", "OnCompleteDestroy",
       "easetype", iTween.EaseType.easeInBack,
       "time", Configuration.instance.destroyTime
       ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
    }

    //Hunter
    void Row_Hunter_Destroy(Item thisItem, Item KomsuItems)
    {


        if (((IsColorHunterBreaker(thisItem.type) && KomsularRowOrColumn() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularCross() && !KomsularHunter())) ||
            ((thisItem.type == ITEM_TYPE.ITEM_ROW || thisItem.type == ITEM_TYPE.ITEM_COLUMN) && KomsularHunter()))

        {
            //StartCoroutine(ColorHunterStartDestroyRow(thisItem));            
            StartCoroutine(Row_Hunter_Destroy_Start(thisItem, KomsuItems));
        }

    }
    IEnumerator Row_Hunter_Destroy_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        //board.lockSwap = true;
        board.HideHint();
        yield return new WaitForSeconds(0.2f);

        Item otheritem = null;
        Item esasoglan = null;
        if ((thisItem.type == ITEM_TYPE.ITEM_ROW || thisItem.type == ITEM_TYPE.ITEM_COLUMN))
        {
            if ((komsuNodeLeft != null) && ((IsColorHunterBreaker(komsuNodeLeft.type))))
            {
                esasoglan = komsuNodeLeft;
                otheritem = thisItem;
            }
            else if ((komsuNodeRight != null) && ((IsColorHunterBreaker(komsuNodeRight.type))))
            {
                esasoglan = komsuNodeRight;
                otheritem = thisItem;
            }
            else if ((komsuNodeTop != null) && ((IsColorHunterBreaker(komsuNodeTop.type))))
            {
                esasoglan = komsuNodeTop;
                otheritem = thisItem;
            }
            else if ((komsuNodeBottom != null) && ((IsColorHunterBreaker(komsuNodeBottom.type))))
            {
                esasoglan = komsuNodeBottom;
                otheritem = thisItem;
            }
        }
        else
        {
            if ((komsuNodeLeft != null) && (komsuNodeLeft.type == ITEM_TYPE.ITEM_ROW || komsuNodeLeft.type == ITEM_TYPE.ITEM_COLUMN))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeLeft;
            }
            else if ((komsuNodeRight != null) && (komsuNodeRight.type == ITEM_TYPE.ITEM_ROW || komsuNodeRight.type == ITEM_TYPE.ITEM_COLUMN))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeRight;
            }
            else if ((komsuNodeTop != null) && (komsuNodeTop.type == ITEM_TYPE.ITEM_ROW || komsuNodeTop.type == ITEM_TYPE.ITEM_COLUMN))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeTop;
            }
            else if ((komsuNodeBottom != null) && (komsuNodeBottom.type == ITEM_TYPE.ITEM_ROW || komsuNodeBottom.type == ITEM_TYPE.ITEM_COLUMN))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeBottom;
            }
        }

        StartMergeBooster(thisItem);
        yield return new WaitForSeconds(0.1f);

        iTween.MoveTo(esasoglan.gameObject, iTween.Hash(
                     "position", gameObject.transform.position,
                     "easetype", iTween.EaseType.easeOutBack,
                     "time", 0.5f //* time
                 ));
        if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();



        iTween.ScaleTo(esasoglan.gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        CameraSize.instance.MedShake = true;
        yield return new WaitForSeconds(0.5f);
        // esasoglan.gameObject.SetActive(false);
        DestroyItemsSameColorRow(esasoglan);


        board.state = GAME_STATE.WAITING_USER_SWAP;
        board.FindMatches();
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("20", PlayerPrefs.GetInt("20") + 1);
            PlayerPrefs.Save();

            //Achievement
            Configuration.SaveAchievement("ach_hunterrow", 1);
        }
        while (board.merging)
        {
            yield return null;
        }
        if (otheritem != null)
            Destroy(otheritem);

        if (esasoglan != null)
            Destroy(esasoglan);

        board.ShowInspiringPopup(3);

    }

    void Cross_Hunter_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((IsColorHunterBreaker(thisItem.type) && !KomsularAnyBomb() && !KomsularColorCone() && KomsularCross() && !KomsularHunter()) ||
                   ((thisItem.type == ITEM_TYPE.ITEM_CROSS && KomsularHunter())))
        {
            StartCoroutine(Cross_Hunter_Destroy_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Cross_Hunter_Destroy_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;

        board.HideHint();
        yield return new WaitForSeconds(0.2f);



        Item otheritem = null;
        Item esasoglan = null;
        if ((thisItem.type == ITEM_TYPE.ITEM_CROSS))
        {
            if ((komsuNodeLeft != null) && (IsColorHunterBreaker(komsuNodeLeft.type)))
            {
                esasoglan = komsuNodeLeft;
                otheritem = thisItem;
            }
            else if ((komsuNodeRight != null) && (IsColorHunterBreaker(komsuNodeRight.type)))
            {
                esasoglan = komsuNodeRight;
                otheritem = thisItem;
            }
            else if ((komsuNodeTop != null) && (IsColorHunterBreaker(komsuNodeTop.type)))
            {
                esasoglan = komsuNodeTop;
                otheritem = thisItem;
            }
            else if ((komsuNodeBottom != null) && (IsColorHunterBreaker(komsuNodeBottom.type)))
            {
                esasoglan = komsuNodeBottom;
                otheritem = thisItem;
            }
        }
        else
        {
            if ((komsuNodeLeft != null) && (komsuNodeLeft.type == ITEM_TYPE.ITEM_CROSS))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeLeft;
            }
            else if ((komsuNodeRight != null) && (komsuNodeRight.type == ITEM_TYPE.ITEM_CROSS))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeRight;
            }
            else if ((komsuNodeTop != null) && (komsuNodeTop.type == ITEM_TYPE.ITEM_CROSS))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeTop;
            }
            else if ((komsuNodeBottom != null) && (komsuNodeBottom.type == ITEM_TYPE.ITEM_CROSS))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeBottom;
            }
        }
        StartMergeBooster(thisItem);
        yield return new WaitForSeconds(0.1f);
        iTween.MoveTo(esasoglan.gameObject, iTween.Hash(
                           "position", gameObject.transform.position,
                           "easetype", iTween.EaseType.easeOutBack,
                           "time", 0.5f //* time
                       ));
        if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();


        iTween.ScaleTo(esasoglan.gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        CameraSize.instance.MedShake = true;
        yield return new WaitForSeconds(0.5f);
        DestroyItemsSameColorCross(esasoglan);
        yield return new WaitForSeconds(1.5f);



        board.state = GAME_STATE.WAITING_USER_SWAP;
        board.FindMatches();
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("19", PlayerPrefs.GetInt("19") + 1);
            PlayerPrefs.Save();
            //Achievement
            Configuration.SaveAchievement("ach_huntercross", 1);
        }
        while (board.merging)
        {
            yield return null;
        }
        if (otheritem != null)
            Destroy(otheritem);

        if (esasoglan != null)
            Destroy(esasoglan);

        board.ShowInspiringPopup(1);

    }

    void Bomb_Hunter_Destroy(Item thisItem, Item KomsuItems)
    {


        if ((IsColorHunterBreaker(thisItem.type) && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB) && KomsularHunter())))

        {


            //StartCoroutine(ColorHunterStartDestroyBomb(thisItem));
            StartCoroutine(Bomb_Hunter_Destroy_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Bomb_Hunter_Destroy_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;

        board.HideHint();
        yield return new WaitForSeconds(0.2f);


        Item otheritem = null;
        Item esasoglan = null;
        if ((thisItem.type == ITEM_TYPE.ITEM_BOMB) || (thisItem.type == ITEM_TYPE.ITEM_MEDBOMB) || (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB))
        {
            if ((komsuNodeLeft != null) && (IsColorHunterBreaker(komsuNodeLeft.type)))
            {
                esasoglan = komsuNodeLeft;
                otheritem = thisItem;
            }
            else if ((komsuNodeRight != null) && (IsColorHunterBreaker(komsuNodeRight.type)))
            {
                esasoglan = komsuNodeRight;
                otheritem = thisItem;
            }
            else if ((komsuNodeTop != null) && (IsColorHunterBreaker(komsuNodeTop.type)))
            {
                esasoglan = komsuNodeTop;
                otheritem = thisItem;
            }
            else if ((komsuNodeBottom != null) && (IsColorHunterBreaker(komsuNodeBottom.type)))
            {
                esasoglan = komsuNodeBottom;
                otheritem = thisItem;
            }
        }
        else
        {
            if ((komsuNodeLeft != null) && (komsuNodeLeft.type == ITEM_TYPE.ITEM_BOMB || komsuNodeLeft.type == ITEM_TYPE.ITEM_MEDBOMB || komsuNodeLeft.type == ITEM_TYPE.ITEM_BIGBOMB))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeLeft;
            }
            else if ((komsuNodeRight != null) && (komsuNodeRight.type == ITEM_TYPE.ITEM_BOMB || komsuNodeRight.type == ITEM_TYPE.ITEM_MEDBOMB || komsuNodeRight.type == ITEM_TYPE.ITEM_BIGBOMB))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeRight;
            }
            else if ((komsuNodeTop != null) && (komsuNodeTop.type == ITEM_TYPE.ITEM_BOMB || komsuNodeTop.type == ITEM_TYPE.ITEM_MEDBOMB || komsuNodeTop.type == ITEM_TYPE.ITEM_BIGBOMB))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeTop;
            }
            else if ((komsuNodeBottom != null) && (komsuNodeBottom.type == ITEM_TYPE.ITEM_BOMB || komsuNodeBottom.type == ITEM_TYPE.ITEM_MEDBOMB || komsuNodeBottom.type == ITEM_TYPE.ITEM_BIGBOMB))
            {
                esasoglan = thisItem;
                otheritem = komsuNodeBottom;
            }
        }
        StartMergeBooster(thisItem);
        yield return new WaitForSeconds(0.1f);
        iTween.MoveTo(esasoglan.gameObject, iTween.Hash(
                           "position", gameObject.transform.position,
                           "easetype", iTween.EaseType.easeOutBack,
                           "time", 0.5f //* time
                       ));
        if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();


        iTween.ScaleTo(esasoglan.gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        CameraSize.instance.MedShake = true;
        yield return new WaitForSeconds(0.5f);
        DestroyItemsSameColorBomb(esasoglan);

        board.state = GAME_STATE.WAITING_USER_SWAP;
        board.FindMatches();
        if (!board.GameOver)
        {
            PlayerPrefs.SetInt("18", PlayerPrefs.GetInt("18") + 1);
            PlayerPrefs.Save();

            //Achievement
            Configuration.SaveAchievement("ach_hunterbomb", 1);
        }
        while (board.merging)
        {
            yield return null;
        }
        if (otheritem != null)
            Destroy(otheritem);

        if (esasoglan != null)
            Destroy(esasoglan);

        board.ShowInspiringPopup(4);

    }
    void Hunter_Hunter_Destroy(Item thisItem, Item KomsuItems)
    {


        if (((IsColorHunterBreaker(thisItem.type) && KomsularHunter())))

        {
            StartCoroutine(Hunter_Hunter_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Hunter_Hunter_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.huntering = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        board.lockSwap = true;
        board.HideHint();
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToRainbow();
        board.state = GAME_STATE.DESTROYING_ITEMS;
        board.lockSwap = true;
        board.HideHint();

        iTween.ScaleTo(gameObject, iTween.Hash(
               "scale", new Vector3(2.5f, 2.5f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        yield return new WaitForSeconds(0.5f);
        board.state = GAME_STATE.DESTROYING_ITEMS;
        board.lockSwap = true;
        board.HideHint();
        board.DoubleRainbowDestroy();

        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        board.ShowInspiringPopup(4);

    }

    void ColorHunterDestroy(Item thisItem)
    {


        if (IsColorHunterBreaker(thisItem.type))// && (KomsularAnyBomb() || KomsularColorCone() || KomsularCross() || KomsularHunter() || KomsularRowOrColumn())) ||
                                                //  (KomsularColorCone()))

        {
            //DestroyItemsSameColor(thisItem);
            StartCoroutine(ColorHunterDestroy_Start(thisItem));
        }

    }
    IEnumerator ColorHunterDestroy_Start(Item thisItem)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        board.HideHint();


        if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
        iTween.ScaleTo(gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        //CameraSize.instance.MedShake = true;
        yield return new WaitForSeconds(0.5f);
        ColorHunterExplosion(thisItem);
        board.ShowInspiringPopup(4);

    }
    //Hunter

    void Cross_Bomb_Destroy(Item thisItem, Item KomsuItems)
    {


        if (((thisItem.type == ITEM_TYPE.ITEM_CROSS)
            && KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            ((thisItem.type == ITEM_TYPE.ITEM_BOMB || thisItem.type == ITEM_TYPE.ITEM_BIGBOMB || thisItem.type == ITEM_TYPE.ITEM_MEDBOMB)) && KomsularCross() && !KomsularAnyBomb() && !KomsularColorCone() && !KomsularHunter())
        {
            StartCoroutine(Cross_Bomb_Start(thisItem, KomsuItems));
        }
    }
    IEnumerator Cross_Bomb_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);
        //ChangeToBig();

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            // "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));

        yield return new WaitForSeconds(0.5f);
        GiantCrossBreakerExplosion();
        if (thisItem != null)
            iTween.ScaleTo(thisItem.gameObject, iTween.Hash(
        "scale", Vector3.zero,
        "onstart", "OnStartDestroy",
        "oncomplete", "OnCompleteDestroy",
        "easetype", iTween.EaseType.easeInBack,
        "time", Configuration.instance.destroyTime
        ));

        if (KomsuItems != null)
            iTween.ScaleTo(KomsuItems.gameObject, iTween.Hash(
         "scale", Vector3.zero,
         "onstart", "OnStartDestroy",
         "oncomplete", "OnCompleteDestroy",
         "easetype", iTween.EaseType.easeInBack,
         "time", Configuration.instance.destroyTime
         ));
        //thisItem.Destroy();
        board.state = GAME_STATE.WAITING_USER_SWAP;
        board.FindMatches();

        board.ShowInspiringPopup(2);

    }

    void Bomb_Bomb_Destroy(Item thisItem, Item KomsuItems)
    {

        if (thisItem.type == ITEM_TYPE.ITEM_BOMB && KomsularBomb() && !KomsularMedBomb() && !KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter())
        {

            StartCoroutine(Bomb_Bomb_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator Bomb_Bomb_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToMedBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
          "scale", new Vector3(3f, 3f, 0),
          // "oncomplete", "CompleteChangeToBig",
          "easetype", iTween.EaseType.easeInOutBack,
          "time", Configuration.instance.changingTime
      ));
        yield return new WaitForSeconds(0.5f);


        thisItem.Destroy();
        board.state = GAME_STATE.WAITING_USER_SWAP;
        board.FindMatches();
        board.ShowInspiringPopup(1);

    }

    void Bomb_Med_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((thisItem.type == ITEM_TYPE.ITEM_BOMB && KomsularMedBomb() && !KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (thisItem.type == ITEM_TYPE.ITEM_MEDBOMB && KomsularBomb() && !KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter())
            )
        {

            StartCoroutine(Bomb_Med_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator Bomb_Med_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToBigBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
          "scale", new Vector3(3f, 3f, 0),
          // "oncomplete", "CompleteChangeToBig",
          "easetype", iTween.EaseType.easeInOutBack,
          "time", Configuration.instance.changingTime
      ));
        yield return new WaitForSeconds(0.5f);

        thisItem.Destroy();
        board.state = GAME_STATE.WAITING_USER_SWAP;

        board.FindMatches();
        board.ShowInspiringPopup(0);

    }

    void Bomb_Big_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((thisItem.type == ITEM_TYPE.ITEM_BOMB && KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB && KomsularBomb() && !KomsularMedBomb() && !KomsularColorCone() && !KomsularHunter())
            )
        {

            StartCoroutine(Bomb_Big_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator Bomb_Big_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToBigBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
         "scale", new Vector3(3f, 3f, 0),
         // "oncomplete", "CompleteChangeToBig",
         "easetype", iTween.EaseType.easeInOutBack,
         "time", Configuration.instance.changingTime
     ));
        yield return new WaitForSeconds(0.5f);
        GiantBombExplosion();

        thisItem.Destroy();
        board.state = GAME_STATE.WAITING_USER_SWAP;

        board.FindMatches();
        board.ShowInspiringPopup(4);

    }

    void Med_Med_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((thisItem.type == ITEM_TYPE.ITEM_MEDBOMB && KomsularMedBomb()) && !KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter())

        {

            StartCoroutine(MedMed_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator MedMed_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToBigBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
         "scale", new Vector3(3f, 3f, 0),
         // "oncomplete", "CompleteChangeToBig",
         "easetype", iTween.EaseType.easeInOutBack,
         "time", Configuration.instance.changingTime
     ));
        yield return new WaitForSeconds(0.5f);
        GiantBombExplosion();

        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        board.ShowInspiringPopup(4);

    }

    void Med_Big_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((thisItem.type == ITEM_TYPE.ITEM_MEDBOMB && KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter()) ||
            (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB && KomsularMedBomb() && !KomsularColorCone() && !KomsularHunter())
            )

        {

            StartCoroutine(MedBig_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator MedBig_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToBigBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
        "scale", new Vector3(3f, 3f, 0),
        // "oncomplete", "CompleteChangeToBig",
        "easetype", iTween.EaseType.easeInOutBack,
        "time", Configuration.instance.changingTime
    ));
        yield return new WaitForSeconds(0.5f);
        MegaGiantBombExplosion();

        board.state = GAME_STATE.WAITING_USER_SWAP;

        board.FindMatches();
        board.ShowInspiringPopup(2);


    }

    void Big_Big_Destroy(Item thisItem, Item KomsuItems)
    {


        if (thisItem.type == ITEM_TYPE.ITEM_BIGBOMB && KomsularBigBomb() && !KomsularColorCone() && !KomsularHunter())

        {

            StartCoroutine(BigBig_Start(thisItem, KomsuItems));

        }

    }
    IEnumerator BigBig_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToBigBomb();

        iTween.ScaleTo(gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        yield return new WaitForSeconds(0.5f);
        MegaGiantBombExplosion();

        board.state = GAME_STATE.WAITING_USER_SWAP;

        board.FindMatches();
        board.ShowInspiringPopup(1);

    }

    void Rainbow_Merge_Destroy(Item thisItem, Item KomsuItems)
    {

        if ((thisItem.type == ITEM_TYPE.ITEM_COLORCONE && (KomsularAnyBomb() || KomsularColorCone() || KomsularCross() || KomsularHunter() || KomsularRowOrColumn())) ||
            (KomsularColorCone()))

        {

            StartCoroutine(Rainbow_Merge_Start(thisItem, KomsuItems));

        }
    }
    IEnumerator Rainbow_Merge_Start(Item thisItem, Item KomsuItems)
    {
        board.merging = true;
        board.state = GAME_STATE.DESTROYING_ITEMS;
        yield return new WaitForSeconds(0.2f);

        StartMergeBooster(thisItem);

        yield return new WaitForSeconds(0.1f);

        ChangeToRainbow();

        iTween.ScaleTo(gameObject, iTween.Hash(
               "scale", new Vector3(3f, 3f, 0),
               // "oncomplete", "CompleteChangeToBig",
               "easetype", iTween.EaseType.easeInOutBack,
               "time", Configuration.instance.changingTime
           ));
        yield return new WaitForSeconds(0.5f);
        //MegaGiantBombExplosion();
        board.DoubleRainbowDestroy();

        board.state = GAME_STATE.WAITING_USER_SWAP;
        //thisItem.Destroy();
        board.FindMatches();
        board.ShowInspiringPopup(4);

    }



    #endregion

    #region Change

    void StartMergeBooster(Item item)
    {
        var combinesBooster = new List<Item>();
        var combinesBoosterType = new List<ITEM_TYPE>();
        combinesBooster = item.node.FindMatchesBooster();



        if (combinesBooster != null)
        {
            foreach (var itm in combinesBooster)
            {
                if (itm != null)// && itm != item)//&& itm.color == item.color)
                {
                    board.MatchesBoosterKomsuList.Add(itm);
                    board.MatchesBoosterKomsuTypeList.Add(itm.type);
                }
            }
        }

        if (item != null)
        {
            foreach (var itm in board.MatchesBoosterKomsuList)
            {
                if (itm != null)
                {

                    iTween.MoveTo(itm.gameObject, iTween.Hash(
                         "position", item.gameObject.transform.position,
                         "easetype", iTween.EaseType.easeOutBack,
                         "time", 0.5f //* time
                     ));
                    if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
                    iTween.ScaleTo(itm.gameObject, iTween.Hash(
                         "scale", Vector3.zero,
                         //"oncomplete", "CompleteChangeToBig",
                         "easetype", iTween.EaseType.easeInOutBack,
                         "time", 0.2f
                     ));
                    //itm.type = ITEM_TYPE.NONE;
                    //itm.Destroy();

                }

            }
        }


        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion3()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = item.gameObject.transform.position;
        }

        board.MatchesBoosterKomsuList.Clear();
        board.MatchesBoosterKomsuTypeList.Clear();

    }
    void StartMerge(GameObject Toplananobje)
    {

        if (komsuNodeLeft != null && IsBoosterItem(komsuNodeLeft.type))
        {
            iTween.MoveTo(komsuNodeLeft.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeLeft.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));
        }
        if (komsuNodeRight != null && IsBoosterItem(komsuNodeRight.type))
        {

            iTween.MoveTo(komsuNodeRight.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeRight.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));

        }
        if (komsuNodeTop != null && IsBoosterItem(komsuNodeTop.type))
        {


            iTween.MoveTo(komsuNodeTop.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeTop.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));


        }
        if (komsuNodeBottom != null && IsBoosterItem(komsuNodeBottom.type))
        {


            iTween.MoveTo(komsuNodeBottom.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeBottom.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));


        }
        if (komsuNodeTopLeft != null && !KomsularRowOrColumn() && !KomsularCross() && IsBoosterItem(komsuNodeTopLeft.type))
        {


            iTween.MoveTo(komsuNodeTopLeft.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeTopLeft.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));


        }
        if (komsuNodeTopRight != null && !KomsularRowOrColumn() && !KomsularCross() && IsBoosterItem(komsuNodeTopRight.type))
        {


            iTween.MoveTo(komsuNodeTopRight.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeTopRight.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));

        }
        if (komsuNodeBottomLeft != null && !KomsularRowOrColumn() && !KomsularCross() && IsBoosterItem(komsuNodeBottomLeft.type))
        {


            iTween.MoveTo(komsuNodeBottomLeft.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeBottomLeft.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));

        }
        if (komsuNodeBottomRight != null && !KomsularRowOrColumn() && !KomsularCross() && IsBoosterItem(komsuNodeBottomRight.type))
        {


            iTween.MoveTo(komsuNodeBottomRight.gameObject, iTween.Hash(
                      "position", gameObject.transform.position,
                      "easetype", iTween.EaseType.easeOutBack,
                      "time", 0.5f //* time
                  ));
            if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            iTween.ScaleTo(komsuNodeBottomRight.gameObject, iTween.Hash(
                 "scale", new Vector3(0.0f, 0.0f, 0),
                 //"oncomplete", "CompleteChangeToBig",
                 "easetype", iTween.EaseType.easeInOutBack,
                 "time", 0.2f
             ));


        }

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.BreakerExplosion3()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

    }
    void StartSizeMin()
    {
        iTween.ScaleTo(komsuNodeLeft.gameObject, iTween.Hash(
             "scale", new Vector3(0.0f, 0.0f, 0),
             //"oncomplete", "CompleteChangeToBig",
             "easetype", iTween.EaseType.easeInOutBack,
             "time", 0.35f
         ));
    }
    void ChangeItemsType(int color, ITEM_TYPE changeToType)
    {
        List<Item> items = board.GetListItems();

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.color == color && item.IsCookie() == true)
                {
                    GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configuration.RainbowExplosion()) as GameObject);

                    if (explosion != null) explosion.transform.position = item.transform.position;

                    if (item.IsColumnBreaker(changeToType) || item.IsRowBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToColRowBreaker();
                        }
                    }
                    else if (item.IsBombBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToBombBreaker();
                        }
                    }
                    else if (item.IsXBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToXBreaker();
                        }
                    }
                    else if (item.IsColorHunterBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToHunterBreaker();
                        }
                    }

                    board.changingList.Add(item);
                }
            }
        }

        board.DestroyChangingList();
    }

    void ChangeToBig()
    {
        if (changing == true) return;
        else changing = true;

        this.GetComponent<SpriteRenderer>().sortingLayerName = "Effect";

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(3f, 3f, 0),
            "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.easeInOutBack,
            "time", Configuration.instance.changingTime
        ));
    }

    void CompleteChangeToBig()
    {
        this.Destroy();

        board.FindMatches();
    }

    #endregion

    #region Drop

    public void Drop()
    {
        if (dropping) return;
        else dropping = true;

        if (dropPath.Count > 1)
        {
            board.droppingItems++;

            var dist = (transform.position.y - dropPath[0].y);


            var time = (transform.position.y - dropPath[dropPath.Count - 1].y) / board.NodeSize();

            while (dist > 0.1f)
            {
                dist -= board.NodeSize();
                dropPath.Insert(0, dropPath[0] + new Vector3(0, board.NodeSize(), 0));
            }

            iTween.MoveTo(gameObject, iTween.Hash(
                "path", dropPath.ToArray(),
                "movetopath", false,
                "onstart", "OnStartDrop",
                "oncomplete", "OnCompleteDrop",
                "easetype", iTween.EaseType.linear,
                "time", Configuration.instance.dropTime / 2 + (time / 10)
            ));
        }
        else
        {
            Vector3 target = board.NodeLocalPosition(node.i, node.j);

            if (Mathf.Abs(transform.position.x - target.x) > 0.1f || Mathf.Abs(transform.position.y - target.y) > 0.1f)
            {
                board.droppingItems++;

                var time = (transform.position.y - target.y) / board.NodeSize();

                iTween.MoveTo(gameObject, iTween.Hash(
                    "position", target,
                    "onstart", "OnStartDrop",
                    "oncomplete", "OnCompleteDrop",
                    "easetype", iTween.EaseType.easeOutBounce,
                    "time", Configuration.instance.dropTime + (time / 10)
                ));
            }
            else
            {
                dropping = false;
            }
        }


    }

    public void OnStartDrop()
    {

    }

    public void OnCompleteDrop()
    {
        if (dropping)
        {
           // AudioManager.instance.DropAudio();

            // reset
            dropPath.Clear();

            board.droppingItems--;

            // reset
            dropping = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - node.i;            
            
        }
       
    }

    #endregion

}
